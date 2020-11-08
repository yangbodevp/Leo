﻿using System;

namespace NMS.Leo.Typed.Core
{
    internal class StaticTypeLeoVisitor : ILeoVisitor
    {
        private readonly DictBase _handler;
        private readonly LeoType _leoType;

        public StaticTypeLeoVisitor(DictBase handler, Type targetType, LeoType leoType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _leoType = leoType;

            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public Type TargetType { get; }

        public bool IsStatic => true;

        public LeoType AlgorithmType => _leoType;

        public void SetValue(string name, object value)
        {
            _handler[name] = value;
        }

        public object GetValue(string name)
        {
            return _handler[name];
        }

        public TValue GetValue<TValue>(string name)
        {
            return _handler.Get<TValue>(name);
        }

        public object this[string name]
        {
            get => _handler[name];
            set => _handler[name] = value;
        }
    }

    internal class StaticTypeLeoVisitor<T> : ILeoVisitor<T>
    {
        private readonly DictBase<T> _handler;
        private readonly LeoType _leoType;

        public StaticTypeLeoVisitor(DictBase<T> handler, LeoType leoType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _leoType = leoType;

            TargetType = typeof(T);
        }

        public Type TargetType { get; }

        public bool IsStatic => true;

        public LeoType AlgorithmType => _leoType;

        public void SetValue(string name, object value)
        {
            _handler[name] = value;
        }

        public object GetValue(string name)
        {
            return _handler[name];
        }

        public TValue GetValue<TValue>(string name)
        {
            return _handler.Get<TValue>(name);
        }

        public object this[string name]
        {
            get => _handler[name];
            set => _handler[name] = value;
        }
    }
}