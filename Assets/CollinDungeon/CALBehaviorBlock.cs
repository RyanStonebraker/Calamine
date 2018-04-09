using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CALBehaviorBlock {
	private string identifier;
	private string returnType; /*nil if return type is void*/
	private Dictionary<string, string> parameters; /*nil if no parameters*/

	string implementation;

	public CALBehaviorBlock()
	{

	}

	public CALBehaviorBlock(ref string signature, int parent)
	{
		int paramStart, paramEnd;
		string aParameterType;

		parameters = new Dictionary<string, string>();
		aParameterType = null; /*Dumb C#*/

		/*Get the return type*/
		returnType = signature.Substring(0, signature.IndexOf(' '));
		signature = signature.Remove(0, signature.IndexOf(' ')+1);
		returnType = returnType.Trim();

		/*Grab the block's identifier*/
		paramStart = signature.IndexOf('(');
		identifier = signature.Substring(0, paramStart);
		signature = signature.Remove(0, paramStart+1);

		/*Parse the paramters*/
		paramEnd = signature.LastIndexOf(')');
		signature = signature.Remove(paramEnd, 1);

		signature = signature.Replace(",", "");

		string[] splitParamters = signature.Split(new System.Char[] { ' ' });

		foreach (string aString in splitParamters) {
			if (aParameterType == null) {
				aParameterType = aString;
				continue;
			}else{
				this.parameters.Add(aString, aParameterType);
				aParameterType = null;
			}
		}

	}
		
}
