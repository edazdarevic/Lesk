using System.Collections.Generic;

namespace Lesk
{
    public class LeskContext
    {
        private int _position;

        public int Position
        {
            get
            {
                return _position;
            }
        }

        public LeskContext(string input)
        {
            Input = input;
            Tokens = new List<Token>();
        }

        public string Input { get; private set; }

        public List<Token> Tokens { get; private set; }

        public char Current
        {
            get { return Input[_position]; }
        }

        public void Advance(int count)
        {
            _position += count;
        }

        public void Advance()
        {
            Advance(1);
        }

        public bool HasMore()
        {
            return _position < Input.Length;
        }

        public LeskContext Clone()
        {
            var context = new LeskContext(Input);
            context._position = _position;
            return context;
        }

        public void Apply(ConsumeResult consumeResult)
        {
            _position += consumeResult.ConsumedLength;
            Tokens.Add(consumeResult.BuildToken());
        }
    }
}