using System;
using DBConsoleNF.Services.OperatorParsers.Interfaces;

namespace DBConsoleNF.Services.OperatorParsers
{
    public class BooleanOperatorParser : BaseOperatorParser, IOperatorParser<bool>
    {
        public override Type OperatorParserType => typeof(bool);
    }
}