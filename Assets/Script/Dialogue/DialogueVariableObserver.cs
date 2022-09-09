using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class DialogueVariableObserver
{

    public static Dictionary<string, Ink.Runtime.Object> variables;

    public DialogueVariableObserver(TextAsset loadGlobalJSON)
    {
        Story globalVariablesStory = new Story(loadGlobalJSON.text);

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value=globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + "=" + value);
        }
    }

    public void startListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += variableChanged;
    }

    public void stopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= variableChanged;
    }



    private void variableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }

    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string,Ink.Runtime.Object>variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public void saveToFile()
    {

    }


}
