using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using WPFLibrary.Interfaces;

namespace WPFLibrary.Tools
{
    internal class OnPropertyChangedInterceptorProxy<T> : RealProxy
        where T : MarshalByRefObject, IInterceptorNotifiable, new()
    {
        private T _proxy;
        private readonly T _target;

        public OnPropertyChangedInterceptorProxy(T target)
            : base(typeof(T))
        {
            this._target = target;
        }

        public override object GetTransparentProxy()
        {
            _proxy = (T)base.GetTransparentProxy();
            return _proxy;
        }


        public override IMessage Invoke(IMessage msg)
        {
            var call = msg as IMethodCallMessage;
            if (call == null)
                throw new NotSupportedException();

            var result = InvokeMethod(call);

            if (call.MethodName.StartsWith("set_"))
                _target.OnPropertyChanged(call.MethodName.Substring(4)); ;

            return result;
        }

        IMethodReturnMessage InvokeMethod(IMethodCallMessage callMsg)
        {
            return RemotingServices.ExecuteMessage(_target, callMsg);
        }

    }
}