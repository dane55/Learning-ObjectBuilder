//===============================================================================
// Microsoft patterns & practices
// ObjectBuilder Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Globalization;
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// һ��ʵ�� <see cref="IBuilderStrategy"/>�ӿڵĳ�����
    /// </summary>
    public abstract class BuilderStrategy : IBuilderStrategy
    {
        /// <summary>
        /// ͨ�ð汾�Ķ��󴴽����԰�����Ԫ����
        /// </summary>
        public TItem BuildUp<TItem>(IBuilderContext context, TItem existing, string idToBuild)
        {
            return (TItem)BuildUp(context, typeof(TItem), existing, idToBuild);
        }

        /// <summary>
        /// ���ڹ����������������е���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="existing">һ��Ĭ�ϴ�null���󴴽��������������д���һ���µĶ���ʵ���������Ϊnull�����������������ж���</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>�����Ķ���</returns>
        public virtual object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            IBuilderStrategy next = context.GetNextInChain(this);

            if (next != null)
                return next.BuildUp(context, typeToBuild, existing, idToBuild);
            else
                return existing;
        }

        /// <summary>
        /// ���ڴݻٶ������������е���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="item">��Ҫ���ٵĶ���ʵ��</param>
        /// <returns>���ص�ǰ���ٵĶ���ʵ��</returns>
        public virtual object TearDown(IBuilderContext context, object item)
        {
            IBuilderStrategy next = context.GetNextInChain(this);

            if (next != null)
                return next.TearDown(context, item);
            else
                return item;
        }

        /// <summary>
        /// ��<see cref="IParameter"/>�����б����������͵ĸ����б�
        /// </summary>
        /// <param name="parameters">��������</param>
        /// <returns>���ز����������͵Ĵ����ַ���</returns>
        protected string ParametersToTypeList(params object[] parameters)
        {
            List<string> types = new List<string>();
            foreach (object parameter in parameters)
            {
                types.Add(parameter.GetType().Name);
            }
            return string.Join(", ", types.ToArray());
        }

        /// <summary>
        /// ���ٵ�����Ϣ��������ʵ��Ĳ���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="typeToBuild">The type being built.</param>
        /// <param name="idToBuild">The ID being built.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The message arguments.</param>
        protected void TraceBuildUp(IBuilderContext context, Type typeToBuild, string idToBuild, string format, params object[] args)
        {
            IBuilderTracePolicy policy = context.Policies.Get<IBuilderTracePolicy>(null, null);

            if (policy != null)
            {
                string message = string.Format(CultureInfo.CurrentCulture, format, args);
                policy.Trace(Properties.Resources.BuilderStrategyTraceBuildUp, GetType().Name, typeToBuild.Name, idToBuild ?? "(null)", message);
            }
        }

        /// <summary>
        /// Traces debugging information, if there is an appropriate policy.
        /// </summary>
        /// <param name="context">The build context.</param>
        /// <param name="item">Item being torn down.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The message arguments.</param>
        protected void TraceTearDown(IBuilderContext context, object item, string format, params object[] args)
        {
            IBuilderTracePolicy policy = context.Policies.Get<IBuilderTracePolicy>(null, null);

            if (policy != null)
            {
                string message = string.Format(CultureInfo.CurrentCulture, format, args);
                policy.Trace(Properties.Resources.BuilderStrategyTraceTearDown, GetType().Name, item.GetType().Name, message);
            }
        }

        /// <summary>
        /// Determines if tracing is enabled
        /// </summary>
        /// <param name="context">The build context.</param>
        /// <returns>Returns true if tracing is enabled; false otherwise.</returns>
        protected bool TraceEnabled(IBuilderContext context)
        {
            return context.Policies.Get<IBuilderTracePolicy>(null, null) != null;
        }
    }
}
