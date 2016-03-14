﻿using System.Collections.Generic;
using System.Linq;

namespace Sharp48.Solvers.Evaluators
{
    public class SumEvaluator : IEvaluator
    {
        private readonly IEnumerable<IEvaluator> _evaluators;

        public SumEvaluator(IEnumerable<IEvaluator> evaluators)
        {
            _evaluators = evaluators;
        }

        public double Evaluate(ulong grid)
            => _evaluators.Aggregate(0D, (current, next) => current + next.Evaluate(grid));
    }
}