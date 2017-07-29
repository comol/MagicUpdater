using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MagicUpdaterCommon.Helpers;

namespace TestPluginOperation1
{
	public class TestPluginOperation1 : OperationWithAttr<PluginAttrib>
	{
		public TestPluginOperation1(int? operationId, string attrsJson = null) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			NLogger.LogDebugToHdd("TestPluginOperation1");
		}
	}

	public class PluginAttrib : IOperationAttributes
	{
		[OperationAttributeDisplayName("Attr1")] // название аттрибута, как оно будет отображаться пользователю
		public string Attr1 { get; set; } = "Value1"; // значение аттрибута

		[OperationAttributeDisplayName("Attr2")] //название аттрибута
		public string Attr2 { get; set; } = "Value2"; //значение аттрибута

	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Тестовая операция с логом на хард"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 3;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "TestPluginOperation1"; //Название операции как оно будет отображаться пользователю
	}
}
