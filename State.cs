namespace Parser
{
    enum State{
        Begin,
        InInteger,
        InFloat,
        InString,
        InStringEscaping,
        InIdentifier,
        InPreComment,
        InComment,
        End
    }
}