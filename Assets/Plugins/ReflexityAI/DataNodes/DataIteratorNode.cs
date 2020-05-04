using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.ReflexityAI.Framework;
using UnityEngine;
using XNode;
using Object = UnityEngine.Object;

namespace Plugins.ReflexityAI.DataNodes {
    public class DataIteratorNode : DataNode, ICacheable {

        [Input(ShowBackingValue.Never, ConnectionType.Override)] public Object[] Array;
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Inherited)] public DataIteratorNode LinkedOption;

        public int Index { get; set; }
        public int CollectionCount => GetCollection().Length;

        private Type _argumentType;
        public Type ArgumentType {
            get {
                if (_argumentType == null)
                    _argumentType = Type.GetType(_typeArgumentName);
                return _argumentType;
            }
        }

        private object _cachedCurrentValue;
        public object CurrentValue {
            get {
                if (_cachedCurrentValue == null && CollectionCount > 0) {
                    _cachedCurrentValue = GetCollection().ElementAt(Index);
                }
                return _cachedCurrentValue;
            }
        }

        private object[] _cachedCollection;
        [SerializeField, HideInInspector] private string _typeArgumentName;
        
        public override void OnCreateConnection(NodePort from, NodePort to) {
            base.OnCreateConnection(from, to);
            if (to.fieldName == nameof(Array) && to.node == this) {
                ReflectionData reflectionData = GetInputValue<ReflectionData>(nameof(Array));
                Type type = reflectionData.Type.GetElementType();
                if (reflectionData.Type.IsArray && type != null) {
                    _typeArgumentName = type.AssemblyQualifiedName;
                    AddDynamicOutput(type, ConnectionType.Multiple, TypeConstraint.Inherited, type.Name);
                } 
                else {
                    Debug.LogError("Iterator can only iterate on array");
                }
            }
        }
        
        public override void OnRemoveConnection(NodePort port) {
            base.OnRemoveConnection(port);
            if (port.fieldName == nameof(Array) && port.node == this) {
                ClearDynamicPorts();
            }
        }

        public override object GetValue(NodePort port) {
            if (port.fieldName == nameof(LinkedOption)) {
                return this;
            } else {
                if (!Application.isPlaying) 
                    return new ReflectionData(ArgumentType, null, true);
                return new ReflectionData(ArgumentType, CurrentValue, true);
            }
        }
        
        private object[] GetCollection() {
            if (_cachedCollection == null)
                _cachedCollection = ((IEnumerable<object>) GetInputValue<ReflectionData>(nameof(Array)).Value).ToArray();
            return _cachedCollection;
        }

        public void ClearCache() {
            _cachedCollection = null;
            _cachedCurrentValue = null;
        }

        public void ClearShortCache() {
            _cachedCurrentValue = null;
        }
        
    }
}