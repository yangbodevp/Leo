﻿using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal abstract class ValueToken : IValueToken
    {
        public static int[] NoMutuallyExclusiveFlags = { };

        // ReSharper disable once InconsistentNaming
        private readonly LeoMember __metadata_LeoMember;

        protected ValueToken(LeoMember member)
        {
            __metadata_LeoMember = member;
        }

        public abstract CorrectValueOps Ops { get; }

        public abstract string TokenName { get; }

        public virtual TokenClass TokenClass => TokenClass.ValueToken;

        public abstract bool MutuallyExclusive { get; }

        public abstract int[] MutuallyExclusiveFlags { get; }

        public abstract CorrectVerifyVal ValidValue(object value);

        protected LeoMember Member => __metadata_LeoMember;
    }

    internal abstract class ValueToken<TVal> : ValueToken, IValueToken<TVal>
    {
        protected ValueToken(LeoMember member) : base(member) { }

        public abstract CorrectVerifyVal ValidValue(TVal value);

        public override CorrectVerifyVal ValidValue(object value)
        {
            if (value is TVal t)
            {
                return ValidValue(t);
            }

            return CorrectVerifyVal.Success;
        }
    }
}