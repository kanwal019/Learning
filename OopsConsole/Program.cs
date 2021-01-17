using OopsConsole.Implementation;

namespace OopsConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var sealedObj = new Classes.SealedClass();
			sealedObj.DisplayRandomNumber();
			sealedObj.DisplayRandomGuid();
		}		
	}
}