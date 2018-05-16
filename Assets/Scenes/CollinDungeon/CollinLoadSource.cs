using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class CALSourceFile
{
	public string sourceFile;
	public string currentLine;
	private StringReader lineGetter;

	public void ReadSourceFile()
	{
		string sourcePath;
		StreamReader fileReader;

		sourcePath = "Assets/CollinDungeon/CalamineTest.txt";
		fileReader = new StreamReader(sourcePath);

		sourceFile = fileReader.ReadToEnd();
		if (sourceFile == "") {
			Debug.Log("Could not open source file!");
			Application.Quit();
		}
		lineGetter = new StringReader(sourceFile);
	}

	public string GetNextLine()
	{
		currentLine = lineGetter.ReadLine();
		if (currentLine == null) {
			Debug.Log("End of file!");
			return null;
		}

		return currentLine;
	}
}
