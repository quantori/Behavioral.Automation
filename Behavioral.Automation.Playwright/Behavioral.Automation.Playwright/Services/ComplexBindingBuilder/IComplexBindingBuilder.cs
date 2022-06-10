using System;

namespace Behavioral.Automation.Playwright.Services.ComplexBindingBuilder
{
    /// <summary>
    /// This interface contain methods which are used to call other bindings inside test methods, so their actions appear in the logs
    /// </summary>
    public interface IComplexBindingBuilder
    {
        void BuildAction(Action method);
        void BuildAction<T>(Action<T> method, params object[] pars);
        void BuildAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, params object[] pars);
        void BuildAction<T1, T2, T3>(Action<T1, T2, T3> method, params object[] pars);
        void BuildAction<T1, T2>(Action<T1, T2> method, params object[] pars);
        void Indent();
        void ReduceIndentation();
    }
}