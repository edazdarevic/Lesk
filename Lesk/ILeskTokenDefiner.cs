using System;

namespace Lesk
{
    public interface ILeskTokenDefiner
    {
        ILeskTokenDefiner DefineToken(string pattern, Func<Token> tokenBuilder, bool caseInsensitive = false);

        ILeskTokenDefiner AsCompiled();

        LeskInstance Done();
    }
}