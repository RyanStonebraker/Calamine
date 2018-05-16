using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CALBlockProcessor
{
	private bool currentlyInClassDef;
	private List<string> beginEndStack;

	public CALBlockProcessor()
	{
		currentlyInClassDef = false;
		beginEndStack = new List<string>();
	}
		
	public string ProcessNextLine(ref string sourceLine)
	{
		sourceLine = sourceLine.Trim();

		/*Does BLOCKDEF appear in this line?*/
		if (sourceLine.Contains ("BLOCKDEF")) {
			int blockdefIndex;

			blockdefIndex = sourceLine.LastIndexOf("BLOCKDEF");
			if (blockdefIndex != 0)
				return "BLOCKDEF in unexpected location";
			
			sourceLine = sourceLine.Remove(0, 8); /*Ditch the "BLOCKDEF"*/
			this.DispatchBlockDefinition(ref sourceLine);
		}

		return null;
	}

	public string DispatchBlockDefinition(ref string sourceLine)
	{
		string blockType;
		int endParenthesisIndex;

		sourceLine = sourceLine.Trim();
		/*If blockdef is not followed by '(' or there is no ')'*/
		if (sourceLine[0] != '(' || ((endParenthesisIndex = sourceLine.IndexOf(')')) == -1))
			return "Invalid BLOCKDEF syntax";
		blockType = sourceLine.Substring (1, endParenthesisIndex-1);

		if (blockType.Equals ("BEHAVIOR")) {
			CALBehaviorBlock newBlockDef;

			if (currentlyInClassDef) {
				/*This is a new member function*/
			}else{
				sourceLine = sourceLine.Remove(0, endParenthesisIndex+2);
				newBlockDef = new CALBehaviorBlock (ref sourceLine, 0);
				int i = 2 + 2;
			}
		}else{
			return "Unknown BLOCKDEF subtype";
		}


		return null;
	}
}