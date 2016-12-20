﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyMonitor.ServiceAccess.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StudyTaskService", Namespace="http://schemas.datacontract.org/2004/07/StudyMonitor.Service")]
    [System.SerializableAttribute()]
    public partial class StudyTaskService : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskTimeSpanService", Namespace="http://schemas.datacontract.org/2004/07/StudyMonitor.Service")]
    [System.SerializableAttribute()]
    public partial class TaskTimeSpanService : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime EndField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TaskIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime End {
            get {
                return this.EndField;
            }
            set {
                if ((this.EndField.Equals(value) != true)) {
                    this.EndField = value;
                    this.RaisePropertyChanged("End");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Start {
            get {
                return this.StartField;
            }
            set {
                if ((this.StartField.Equals(value) != true)) {
                    this.StartField = value;
                    this.RaisePropertyChanged("Start");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TaskId {
            get {
                return this.TaskIdField;
            }
            set {
                if ((this.TaskIdField.Equals(value) != true)) {
                    this.TaskIdField = value;
                    this.RaisePropertyChanged("TaskId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IStudyTasksService")]
    public interface IStudyTasksService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/Add", ReplyAction="http://tempuri.org/IStudyTasksService/AddResponse")]
        int Add(StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService task);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/Add", ReplyAction="http://tempuri.org/IStudyTasksService/AddResponse")]
        System.Threading.Tasks.Task<int> AddAsync(StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService task);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/GetTask", ReplyAction="http://tempuri.org/IStudyTasksService/GetTaskResponse")]
        StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService GetTask(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/GetTask", ReplyAction="http://tempuri.org/IStudyTasksService/GetTaskResponse")]
        System.Threading.Tasks.Task<StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService> GetTaskAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/AddTimeSpanTo", ReplyAction="http://tempuri.org/IStudyTasksService/AddTimeSpanToResponse")]
        int AddTimeSpanTo(int taskId, StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService timeSpan);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/AddTimeSpanTo", ReplyAction="http://tempuri.org/IStudyTasksService/AddTimeSpanToResponse")]
        System.Threading.Tasks.Task<int> AddTimeSpanToAsync(int taskId, StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService timeSpan);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/GetTimeSpansFor", ReplyAction="http://tempuri.org/IStudyTasksService/GetTimeSpansForResponse")]
        StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService[] GetTimeSpansFor(int taskId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/GetTimeSpansFor", ReplyAction="http://tempuri.org/IStudyTasksService/GetTimeSpansForResponse")]
        System.Threading.Tasks.Task<StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService[]> GetTimeSpansForAsync(int taskId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/ClearAll", ReplyAction="http://tempuri.org/IStudyTasksService/ClearAllResponse")]
        void ClearAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudyTasksService/ClearAll", ReplyAction="http://tempuri.org/IStudyTasksService/ClearAllResponse")]
        System.Threading.Tasks.Task ClearAllAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStudyTasksServiceChannel : StudyMonitor.ServiceAccess.ServiceReference.IStudyTasksService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StudyTasksServiceClient : System.ServiceModel.ClientBase<StudyMonitor.ServiceAccess.ServiceReference.IStudyTasksService>, StudyMonitor.ServiceAccess.ServiceReference.IStudyTasksService {
        
        public StudyTasksServiceClient() {
        }
        
        public StudyTasksServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StudyTasksServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StudyTasksServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StudyTasksServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Add(StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService task) {
            return base.Channel.Add(task);
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService task) {
            return base.Channel.AddAsync(task);
        }
        
        public StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService GetTask(int id) {
            return base.Channel.GetTask(id);
        }
        
        public System.Threading.Tasks.Task<StudyMonitor.ServiceAccess.ServiceReference.StudyTaskService> GetTaskAsync(int id) {
            return base.Channel.GetTaskAsync(id);
        }
        
        public int AddTimeSpanTo(int taskId, StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService timeSpan) {
            return base.Channel.AddTimeSpanTo(taskId, timeSpan);
        }
        
        public System.Threading.Tasks.Task<int> AddTimeSpanToAsync(int taskId, StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService timeSpan) {
            return base.Channel.AddTimeSpanToAsync(taskId, timeSpan);
        }
        
        public StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService[] GetTimeSpansFor(int taskId) {
            return base.Channel.GetTimeSpansFor(taskId);
        }
        
        public System.Threading.Tasks.Task<StudyMonitor.ServiceAccess.ServiceReference.TaskTimeSpanService[]> GetTimeSpansForAsync(int taskId) {
            return base.Channel.GetTimeSpansForAsync(taskId);
        }
        
        public void ClearAll() {
            base.Channel.ClearAll();
        }
        
        public System.Threading.Tasks.Task ClearAllAsync() {
            return base.Channel.ClearAllAsync();
        }
    }
}
