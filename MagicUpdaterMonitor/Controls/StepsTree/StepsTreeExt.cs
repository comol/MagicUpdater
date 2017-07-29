using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StepTreeControl
{
	public partial class StepsTree
	{
		private const int MILLISECONDS_IN_SECOND = 1000;
		public void PopulateA(IEnumerable<ViewShedulerStepsModelParentChild> source)
		{
			const int ROOT_STEP_ID = 0;
			AddNodes(stepTree.Nodes, ROOT_STEP_ID, source);
		}
		private void AddNodes(TreeNodeCollection parentNodes, int parentId, IEnumerable<ViewShedulerStepsModelParentChild> source)
		{
			const int IMAGE_INDEX_POSITIVE = 0;
			const int IMAGE_INDEX_NEGATIVE = 1;

			foreach (var item in source.Where(item => item.ParentId == parentId))
			{
				var stepProperties = GetStepProperties(item.StepData, 0, item.IsChildPositiveBranch);
				var newNode = new TreeNode
				{
					Text = stepProperties.OperationName,
					ImageIndex = (item.IsChildPositiveBranch) ? IMAGE_INDEX_POSITIVE : IMAGE_INDEX_NEGATIVE,
					SelectedImageIndex = (item.IsChildPositiveBranch) ? IMAGE_INDEX_POSITIVE : IMAGE_INDEX_NEGATIVE,
					Tag = stepProperties
				};

				parentNodes.Add(newNode);
				AddNodes(newNode.Nodes, item.ChildId.Value, source);
			}
		}

		private StepsProperties GetStepProperties(ShedulerStep step, int parentStepId, bool isPositive)
		{
			if (step == null)
			{
				return null;
			}
			return new StepsProperties
			{
				Id = step.Id,
				ParentId = parentStepId,
				IsPositive = isPositive,
				OperationId = step.OperationType,
				OperationName = MQueryCommand.GetOperationNameRusById(step.OperationType),
				WaitingForSuccessInterval = step.OperationCheckIntervalMs / MILLISECONDS_IN_SECOND,
				RepeatingIntervalOn1cWaiting = (step.RepeatTimeout ?? 0) / MILLISECONDS_IN_SECOND,
				RepeatTimesOnLackOf1cResponse = step.RepeatCount ?? 0
			};
		}

		public bool AddSteps(int taskId)
		{
			//Добавление шагов
			if (!this.IsEmpty())
			{
				var allStepNodes = this.GetAllNodes();

				int firstStepId = 0;
				foreach (var stepNode in allStepNodes)
				{
					if (stepNode.Tag == null)
					{
						return false;
					}
					var currentStep = (StepsProperties)stepNode.Tag;
					ShedulerStep shedulerStep = MQueryCommand.SelectShedulerStep(currentStep.Id);
					TrySQLCommand insertStepResult = null;
					if (shedulerStep == null)
					{
						insertStepResult = MQueryCommand.TryInsertNewStep(taskId,
																	currentStep.OperationId,
																	currentStep.RepeatTimesOnLackOf1cResponse,
																	currentStep.RepeatingIntervalOn1cWaiting * MILLISECONDS_IN_SECOND,
																	currentStep.WaitingForSuccessInterval * MILLISECONDS_IN_SECOND);
					}
					else
					{
						insertStepResult = MQueryCommand.TryUpdateStep(currentStep.Id,
																	currentStep.OperationId,
																	currentStep.RepeatTimesOnLackOf1cResponse,
																	currentStep.RepeatingIntervalOn1cWaiting * MILLISECONDS_IN_SECOND,
																	currentStep.WaitingForSuccessInterval * MILLISECONDS_IN_SECOND);
					}

					if (!insertStepResult.IsComplete)
					{
						MessageBox.Show($"Ошибка добавления новой задачи{Environment.NewLine}{insertStepResult.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					if (stepNode.Parent == null)
					{
						firstStepId = insertStepResult.ReturnedId;
					}

					currentStep.Id = insertStepResult.ReturnedId;
				};
				var uspdateResult = MQueryCommand.TryUpdateStepFirstStepId(taskId, firstStepId);
				if (!uspdateResult.IsComplete)
				{
					return false;
				}

				// Обновляем Id связей
				foreach (var stepNode in allStepNodes)
				{
					var stepNodeProperties = (StepsProperties)stepNode.Tag;
					foreach (TreeNode childNode in stepNode.Nodes)
					{
						var childNodeProperties = (StepsProperties)childNode.Tag;
						if (childNodeProperties.IsPositive)
						{
							uspdateResult = MQueryCommand.TryUpdateStepPositiveOperationId(stepNodeProperties.Id, childNodeProperties.Id);
						}
						else
						{
							uspdateResult = MQueryCommand.TryUpdateStepNegativeOperationId(stepNodeProperties.Id, childNodeProperties.Id);
						}
						if (!uspdateResult.IsComplete)
						{
							return false;
						}
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
