﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TTDepartment.TTSvc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TTSvc.ITTServ")]
    public interface ITTServ {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/HelloWorld", ReplyAction="http://tempuri.org/ITTServ/HelloWorldResponse")]
        string HelloWorld(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/HelloWorld", ReplyAction="http://tempuri.org/ITTServ/HelloWorldResponse")]
        System.Threading.Tasks.Task<string> HelloWorldAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/AddDepartment", ReplyAction="http://tempuri.org/ITTServ/AddDepartmentResponse")]
        object AddDepartment(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/AddDepartment", ReplyAction="http://tempuri.org/ITTServ/AddDepartmentResponse")]
        System.Threading.Tasks.Task<object> AddDepartmentAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/AddTicket", ReplyAction="http://tempuri.org/ITTServ/AddTicketResponse")]
        object AddTicket(string title, string description, int author, System.Nullable<int> parent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/AddTicket", ReplyAction="http://tempuri.org/ITTServ/AddTicketResponse")]
        System.Threading.Tasks.Task<object> AddTicketAsync(string title, string description, int author, System.Nullable<int> parent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/AssignTicket", ReplyAction="http://tempuri.org/ITTServ/AssignTicketResponse")]
        object AssignTicket(int id, int assignee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/AssignTicket", ReplyAction="http://tempuri.org/ITTServ/AssignTicketResponse")]
        System.Threading.Tasks.Task<object> AssignTicketAsync(int id, int assignee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/GetTicket", ReplyAction="http://tempuri.org/ITTServ/GetTicketResponse")]
        object GetTicket(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITTServ/GetTicket", ReplyAction="http://tempuri.org/ITTServ/GetTicketResponse")]
        System.Threading.Tasks.Task<object> GetTicketAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITTServChannel : TTDepartment.TTSvc.ITTServ, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TTServClient : System.ServiceModel.ClientBase<TTDepartment.TTSvc.ITTServ>, TTDepartment.TTSvc.ITTServ {
        
        public TTServClient() {
        }
        
        public TTServClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TTServClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TTServClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TTServClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld(string name) {
            return base.Channel.HelloWorld(name);
        }
        
        public System.Threading.Tasks.Task<string> HelloWorldAsync(string name) {
            return base.Channel.HelloWorldAsync(name);
        }
        
        public object AddDepartment(string name) {
            return base.Channel.AddDepartment(name);
        }
        
        public System.Threading.Tasks.Task<object> AddDepartmentAsync(string name) {
            return base.Channel.AddDepartmentAsync(name);
        }
        
        public object AddTicket(string title, string description, int author, System.Nullable<int> parent) {
            return base.Channel.AddTicket(title, description, author, parent);
        }
        
        public System.Threading.Tasks.Task<object> AddTicketAsync(string title, string description, int author, System.Nullable<int> parent) {
            return base.Channel.AddTicketAsync(title, description, author, parent);
        }
        
        public object AssignTicket(int id, int assignee) {
            return base.Channel.AssignTicket(id, assignee);
        }
        
        public System.Threading.Tasks.Task<object> AssignTicketAsync(int id, int assignee) {
            return base.Channel.AssignTicketAsync(id, assignee);
        }
        
        public object GetTicket(int id) {
            return base.Channel.GetTicket(id);
        }
        
        public System.Threading.Tasks.Task<object> GetTicketAsync(int id) {
            return base.Channel.GetTicketAsync(id);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TTSvc.IAuthServ")]
    public interface IAuthServ {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/login", ReplyAction="http://tempuri.org/IAuthServ/loginResponse")]
        string login(string email, string hash);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/login", ReplyAction="http://tempuri.org/IAuthServ/loginResponse")]
        System.Threading.Tasks.Task<string> loginAsync(string email, string hash);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/register", ReplyAction="http://tempuri.org/IAuthServ/registerResponse")]
        string register(string name, string email, string hash, string salt, int department);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/register", ReplyAction="http://tempuri.org/IAuthServ/registerResponse")]
        System.Threading.Tasks.Task<string> registerAsync(string name, string email, string hash, string salt, int department);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/logout", ReplyAction="http://tempuri.org/IAuthServ/logoutResponse")]
        string logout(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/logout", ReplyAction="http://tempuri.org/IAuthServ/logoutResponse")]
        System.Threading.Tasks.Task<string> logoutAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/getSalt", ReplyAction="http://tempuri.org/IAuthServ/getSaltResponse")]
        string getSalt(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthServ/getSalt", ReplyAction="http://tempuri.org/IAuthServ/getSaltResponse")]
        System.Threading.Tasks.Task<string> getSaltAsync(string email);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthServChannel : TTDepartment.TTSvc.IAuthServ, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthServClient : System.ServiceModel.ClientBase<TTDepartment.TTSvc.IAuthServ>, TTDepartment.TTSvc.IAuthServ {
        
        public AuthServClient() {
        }
        
        public AuthServClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthServClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthServClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthServClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string login(string email, string hash) {
            return base.Channel.login(email, hash);
        }
        
        public System.Threading.Tasks.Task<string> loginAsync(string email, string hash) {
            return base.Channel.loginAsync(email, hash);
        }
        
        public string register(string name, string email, string hash, string salt, int department) {
            return base.Channel.register(name, email, hash, salt, department);
        }
        
        public System.Threading.Tasks.Task<string> registerAsync(string name, string email, string hash, string salt, int department) {
            return base.Channel.registerAsync(name, email, hash, salt, department);
        }
        
        public string logout(string token) {
            return base.Channel.logout(token);
        }
        
        public System.Threading.Tasks.Task<string> logoutAsync(string token) {
            return base.Channel.logoutAsync(token);
        }
        
        public string getSalt(string email) {
            return base.Channel.getSalt(email);
        }
        
        public System.Threading.Tasks.Task<string> getSaltAsync(string email) {
            return base.Channel.getSaltAsync(email);
        }
    }
}
