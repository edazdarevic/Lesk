## What is Lesk?
Lesk is a lexical analyzer library. It helps you easily build your lexical analyzers at runtime. Lexical analyzers are also called lexers or tokenizers or scanners. Lexer transforms a sequence of characters to a sequence of tokens.

## How to use Lesk?
Lesk uses regular expressions for defining tokens. For example a JSON lexer would be defined with something like this: 

    var jsonLexer = LeskInstance.Configure
                  .DefineToken(@"\s+", () => new WhitespaceToken())
                  .DefineToken(":", () => new ColonToken())
                  .DefineToken(",", () => new CommaToken())
                  .DefineToken("{", () => new LBraceToken())
                  .DefineToken("}", () => new RBraceToken())
                  .DefineToken("true", () => new TrueFalseToken())
                  .DefineToken("false", () => new TrueFalseToken())
                  .DefineToken("null", () => new NullToken())
                  .DefineToken("-?[0-9]+", () => new IntToken())
                  .DefineToken("\".*?\"", () => new StringToken())
                  .DefineToken(@"(-?[0-9]+)(\.[0-9]+)",()=> new DoubleToken())
                  .AsCompiled()
                  .Done();

To perform actual lexing call the `Tokenize` method 

    List<Token> tokens = jsonLexer.Tokenize(yourStringHere); 

## How is Lesk implemented?
Lesk internally relies on default .NET regular expression implementation. However, this might change in the future.

## Roadmap 
0.4

* Support for tokenizing directly from a Stream and yielding tokens as they become available 

0.5 etc.

* Recursive definition of tokens 
* Other cool stuff

## License 
Lesk is released under MIT License.
