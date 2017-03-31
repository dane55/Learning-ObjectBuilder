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
using System.Diagnostics;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="IBuilderTracePolicy"/> ͨ��<see cref="TraceSource"/>��¼������Ϣ 
    /// </summary>
    public class BuilderTraceSourcePolicy : IBuilderTracePolicy
    {
        TraceSource traceSource;

        /// <summary>
        /// ʵ���� <see cref="BuilderTraceSourcePolicy"/> ��
        /// </summary>
        public BuilderTraceSourcePolicy(TraceSource traceSource)
        {
            this.traceSource = traceSource;
        }

        /// <summary>
        ///  ʹ��ָ���Ķ�������͸�ʽ����Ϣ������Ϣ����Ϣд�� System.Diagnostics.TraceSource.Listeners �����еĸ����������С�
        /// </summary>
        public void Trace(string format, params object[] args)
        {
            traceSource.TraceInformation(format, args);
        }
    }
}
