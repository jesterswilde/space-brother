using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CharacterExpression {

    Action<List<string>> updateSkillsDB;
    Action<List<string>> updatePreferenceDB;

    Dictionary<ExpressionEnum, float> skills = new Dictionary<ExpressionEnum, float>();
    Dictionary<ExpressionEnum, float> preferences = new Dictionary<ExpressionEnum, float>(); 

    public float GetPreference(ExpressionEnum expression)
    {
        return preferences[expression]; 
    }

    public int ExprsesionSkillnRoll(ExpressionEnum expression)
    {
        return Roll.Bias(5, 20, (int)skills[expression]); 
    }

    void GenerateExpression()
    {
        Util.EnumList<ExpressionEnum>()
            .ForEach((expression) =>
            {
                skills[expression] = Roll.Bias(5, 20);
                preferences[expression] = Roll.Bias(5, 20);
            });
        SerializeExpression(); 
    }
    void SerializeExpression()
    {
        updateSkillsDB(skills.Stringify());
        updatePreferenceDB(preferences.Stringify()); 
    }

	public CharacterExpression Initialize(Action<List<string>> skills, Action<List<string>>preference)
    {
        updateSkillsDB = skills;
        updatePreferenceDB = preference;
        GenerateExpression(); 
        return this; 
    }
}
