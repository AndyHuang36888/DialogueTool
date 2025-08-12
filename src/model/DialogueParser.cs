namespace DialogueSystem;

using System;
using System.ComponentModel;

class DialougeParser
{
    private static readonly DialougeParser instance = new DialougeParser();

    const string FLAG_GO_TO = ">>";
    const string FLAG_CHOICE = "-";

    public DialogueNode CurrentNode { get; }

    private DialougeParser()
    {

    }

    public static DialougeParser Instance()
    {
        return instance;
    }

    // reads file from path
    public void ReadFile(string path)
    {

    }

    // Determines the line type and writes the data into current node or finishes node
    public void ParseLine(string line)
    {
        // reads line
        // text, choice, or command
        // is curr node over?
        // yes -> end creation and make new
        // no-> continue
    }

    // Determines if the current node is finished using the current line
    public void IsNodeFinished()
    {
        // User flags node is finished -> end
        // New Name given -> end
        // Text after choices -> end

    }
}

internal class DialougeParserinstance
{
}