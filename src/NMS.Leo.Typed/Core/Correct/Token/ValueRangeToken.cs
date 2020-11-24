﻿using System;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueRangeToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "Value range rule";
        private readonly IComparable _from;
        private readonly IComparable _to;

        private bool _returnFalseDirectly;

        public ValueRangeToken(LeoMember member, object from, object to) : base(member)
        {
            if (from is null || to is null)
            {
                _from = default;
                _to = default;
                _returnFalseDirectly = true;
            }

            if (!_returnFalseDirectly && from is IComparable from0)
            {
                _from = from0;
            }
            else
            {
                _from = default;
                _returnFalseDirectly = true;
            }

            if (!_returnFalseDirectly && to is IComparable to0)
            {
                _to = to0;
            }
            else
            {
                _to = default;
                _returnFalseDirectly = true;
            }

            if (!_returnFalseDirectly && _from!.CompareTo(_to) > 0)
            {
                _returnFalseDirectly = true;
            }
        }

        public override CorrectValueOps Ops => CorrectValueOps.Range;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (_returnFalseDirectly)
            {
                UpdateVal(val, value);
            }

            else if (value is null)
            {
                UpdateVal(val, null);
            }

            else if (value is char c)
            {
                var fromC = Convert.ToChar(_from);
                var toC = Convert.ToChar(_to);

                if (c < fromC || c > toC)
                {
                    UpdateVal(val, c);
                }
            }

            else if (Member.MemberType.IsPrimitive && Member.MemberType.IsValueType)
            {
                var d = Convert.ToDecimal(value);
                var fromD = Convert.ToDecimal(_from);
                var toD = Convert.ToDecimal(_to);

                if (d < fromD || d > toD)
                {
                    UpdateVal(val, d);
                }
            }

            else if (value is IComparable comparable)
            {
                if (comparable.CompareTo(_from) < 0 || comparable.CompareTo(_to) > 0)
                {
                    UpdateVal(val, comparable);
                }
            }

            else
            {
                UpdateVal(val, value, "The given value cannot be compared.");
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = message ?? $"The given value is not in the valid range. The current value is: {obj}, and the valid range is from {_from} to {_to}.";
        }

        public override string ToString() => NAME;
    }
}