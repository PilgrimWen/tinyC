namespace Parser
{
    enum State{
        Begin,
        InInteger,
        InFloat,
        InString,
        InStringEscaping,
        InIdentifier,
        End
    }
}