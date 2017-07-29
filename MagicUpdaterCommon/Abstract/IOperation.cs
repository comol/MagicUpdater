namespace MagicUpdaterCommon.Abstract
{
	//Да пустые. Нужно сдля системы плагинов!
	public interface IOperation
	{
	}

	public interface IOperationAttributes
	{
	}

	public interface IRegistrationParams
	{
		string NameRus { get; }
		int GroupId { get; }
		string Description { get; }
	}
}
