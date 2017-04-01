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
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// һ�����������ö�����ֵ�ṹ����ʾ��ֵ�洢Ϊ�����ö�����ǿ���õ��ֵ䣬֧�ֿ�ֵ
    /// </summary>
    /// <typeparam name="TKey">������</typeparam>
    /// <typeparam name="TValue">ֵ����</typeparam>
    public class WeakRefDictionary<TKey, TValue>
    {
        /// <summary>
        /// ���������ͼ��ϣ���ʾ�����ã��������ö����ͬʱ��Ȼ�����������������ոö���
        /// </summary>
        private Dictionary<TKey, WeakReference> inner = new Dictionary<TKey, WeakReference>();

        /// <summary>
        /// ʵ���� <see cref="WeakRefDictionary{K,V}"/> ��
        /// </summary>
        public WeakRefDictionary()
        {
        }

        /// <summary>
        /// ���ֵ��м���ֵ
        /// </summary>
        /// <param name="key">KeyΨһ��ʶ</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                TValue result;

                if (TryGet(key, out result))
                    return result;

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// �����ֵ��������ļ���
        /// </summary>
        /// <remarks>�����ֵ��е����������ó��У����Բ�����������ֵ����֤ͨ��ö�ٽ����ֵĶ������Ŀ��ֻ�������������һ������ֵ</remarks>
        public int Count
        {
            get
            {
                CleanAbandonedItems();
                return inner.Count;
            }
        }

        /// <summary>
        /// ���ֵ��������
        /// </summary>
        /// <param name="key">KeyΨһ��ʶ</param>
        /// <param name="value">��ӵ�ֵ</param>
        public void Add(TKey key, TValue value)
        {
            TValue dummy;
            if (TryGet(key, out dummy)) { throw new ArgumentException(Properties.Resources.KeyAlreadyPresentInDictionary); }
            inner.Add(key, new WeakReference(EncodeNullObject(value)));
        }

        /// <summary>
        /// ȷ���ֵ��Ƿ��������ֵ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            TValue dummy;
            return TryGet(key, out dummy);
        }

        /// <summary>
        /// ����һ��ѭ�����ʼ��ϵ�ö������
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (KeyValuePair<TKey, WeakReference> kvp in inner)
            {
                object innerValue = kvp.Value.Target;

                if (innerValue != null)
                    yield return new KeyValuePair<TKey, TValue>(kvp.Key, DecodeNullObject<TValue>(innerValue));
            }
        }

        /// <summary>
        /// ���ֵ����Ƴ���
        /// </summary>
        /// <param name="key">The key of the item to be removed.</param>
        /// <returns>Returns true if the key was in the dictionary; return false otherwise.</returns>
        public bool Remove(TKey key)
        {
            return inner.Remove(key);
        }

        /// <summary>
        /// ��ͼ���ֵ��еõ�һ��ֵ
        /// </summary>
        /// <param name="key">KeyΨһ��ʶ</param>
        /// <param name="value">��ӵ�ֵ</param>
        /// <returns>��ֵ���ڣ��򷵻�true������false</returns>
        public bool TryGet(TKey key, out TValue value)
        {
            value = default(TValue);
            WeakReference wr;

            if (!inner.TryGetValue(key, out wr))
                return false;

            object result = wr.Target;

            if (result == null)
            {
                inner.Remove(key);
                return false;
            }

            value = DecodeNullObject<TValue>(result);
            return true;
        }

        private void CleanAbandonedItems()
        {
            List<TKey> deadKeys = new List<TKey>();

            foreach (KeyValuePair<TKey, WeakReference> kvp in inner)
                if (kvp.Value.Target == null)
                    deadKeys.Add(kvp.Key);

            foreach (TKey key in deadKeys)
                inner.Remove(key);
        }

        private TObject DecodeNullObject<TObject>(object innerValue)
        {
            if (innerValue == typeof(NullObject))
                return default(TObject);
            else
                return (TObject)innerValue;
        }

        private object EncodeNullObject(object value)
        {
            if (value == null)
                return typeof(NullObject);
            else
                return value;
        }

        private class NullObject
        {
        }
    }
}