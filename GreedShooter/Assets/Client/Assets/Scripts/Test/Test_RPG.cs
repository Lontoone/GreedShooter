using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Test_RPG : MonoBehaviour
{
    public TextAsset s;
    public void Start()
    {
        string data = s.text;
        Regex regex = new Regex(RPGCore.REGEX_SPLIT_LINES,
                                          RegexOptions.IgnoreCase
                                         | RegexOptions.Compiled
                                         | RegexOptions.Multiline

                                         | RegexOptions.IgnorePatternWhitespace);


        Match match = regex.Match(data);
        while (match.Success)
        {
            GroupCollection groups = match.Groups;
            Debug.Log(groups["tag"].Value +" "+
                                      groups["arg"].Value +" "+
                                      groups["text"].Value);


            match = match.NextMatch();
        }


    }
}
