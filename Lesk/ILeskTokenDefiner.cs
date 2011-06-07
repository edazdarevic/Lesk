using System;

namespace Lesk
{
    public interface ILeskTokenDefiner
    {
        ILeskTokenDefiner DefineToken(string pattern, Func<Token> tokenBuilder);

        ILeskTokenDefiner AsCompiled();

        LeskInstance Done();
    }
}