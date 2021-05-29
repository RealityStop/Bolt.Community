﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Bolt;
using Bolt.Addons.Community.Fundamentals.Units.logic;

namespace Bolt.Addons.Community.Utility
{
    [IncludeInSettings(true)]
    public class AOTAction : IAction
    {
        public Action action;
        public object GetDelegate() => action;
        public Type GetDelegateType() => typeof(Action);
        public TypeParam[] parameters => new TypeParam[] { };
        public virtual string DisplayName => "AOT Action";

        public void Invoke(params object[] parameters)
        {
            action.Invoke();
        }

        public void Initialize(Flow flow, ActionUnit unit, Action action)
        {
            this.action = () => { action.Invoke(); };
        }

        public void Bind(object a, object b)
        {
            var _action = a as Action;
            _action += ((Action)b);
        }

        public void Unbind(object a, object b)
        {
            var _action = a as Action;
            _action -= ((Action)b);
        }
    }

    [IncludeInSettings(true)]
    public abstract class AOTAction<T> : IAction
    {
        public Action<T> action;
        public object GetDelegate() => action;
        public Type GetDelegateType() => typeof(Action<T>);
        public abstract string DisplayName { get; }

        public abstract TypeParam[] parameters { get; }

        public void Invoke(params object[] parameters)
        {
            action.Invoke((T)parameters[0]);
        }

        public void Initialize(Flow flow, ActionUnit unit, Action flowAction)
        {
            this.action = (t) =>
            {
                unit.AssignParameters(flow, t);
                flowAction.Invoke();
            };
        }

        public void Bind(object a, object b)
        {
            var _action = a as Action<T>;
            _action += ((Action<T>)b);
        }

        public void Unbind(object a, object b)
        {
            var _action = a as Action<T>;
            _action -= ((Action<T>)b);
        }
    }

    [IncludeInSettings(true)]
    public abstract class AOTAction<T1, T2> : IAction
    {
        internal Action<T1, T2> action;
        public object GetDelegate() => action;
        public Type GetDelegateType() => typeof(Action<T1, T2>);
        public abstract string DisplayName { get; }

        public abstract TypeParam[] parameters { get; }

        public void Invoke(params object[] parameters)
        {
            action.Invoke((T1)parameters[0], (T2)parameters[1]);
        }

        public void Initialize(Flow flow, ActionUnit unit, Action flowAction)
        {
            this.action = (t1, t2) =>
            {
                unit.AssignParameters(flow, t1, t2);
                flowAction.Invoke();
            };
        }

        public void Bind(object a, object b)
        {
            var _action = a as Action<T1, T2>;
            _action += ((Action<T1, T2>)b);
        }

        public void Unbind(object a, object b)
        {
            var _action = a as Action<T1, T2>;
            _action -= ((Action<T1, T2>)b);
        }
    }

    [IncludeInSettings(true)]
    public abstract class AOTAction<T1, T2, T3> : IAction
    {
        public Action<T1, T2, T3> action;
        public object GetDelegate() => action;
        public Type GetDelegateType() => typeof(Action<T1, T2, T3>);
        public abstract string DisplayName { get; }

        public abstract TypeParam[] parameters { get; }

        public void Invoke(params object[] parameters)
        {
            action.Invoke((T1)parameters[0], (T2)parameters[1], (T3)parameters[3]);
        }

        public void Initialize(Flow flow, ActionUnit unit, Action flowAction)
        {
            this.action = (t1, t2, t3) =>
            {
                unit.AssignParameters(flow, t1, t2, t3);
                flowAction.Invoke();
            };
        }

        public void Bind(object a, object b)
        {
            var _action = a as Action<T1, T2, T3>;
            _action += ((Action<T1, T2, T3>)b);
        }

        public void Unbind(object a, object b)
        {
            var _action = a as Action<T1, T2, T3>;
            _action -= ((Action<T1, T2, T3>)b);
        }
    }

    [IncludeInSettings(true)]
    public abstract class AOTAction<T1, T2, T3, T4> : IAction
    {
        public Action<T1, T2, T3, T4> action;
        public object GetDelegate() => action;
        public Type GetDelegateType() => typeof(Action<T1, T2, T3, T4>);
        public abstract string DisplayName { get; }

        public abstract TypeParam[] parameters { get; }

        public void Invoke(params object[] parameters)
        {
            action.Invoke((T1)parameters[0], (T2)parameters[1], (T3)parameters[3], (T4)parameters[4]);
        }

        public void Initialize(Flow flow, ActionUnit unit, Action flowAction)
        {
            this.action = (t1, t2, t3, t4) =>
            {
                unit.AssignParameters(flow, t1, t2, t3, t4);
                flowAction.Invoke();
            };
        }

        public void Bind(object a, object b)
        {
            var _action = a as Action<T1, T2, T3, T4>;
            _action += ((Action<T1, T2, T3, T4>)b);
        }

        public void Unbind(object a, object b)
        {
            var _action = a as Action<T1, T2, T3, T4>;
            _action -= ((Action<T1, T2, T3, T4>)b);
        }
    }
}