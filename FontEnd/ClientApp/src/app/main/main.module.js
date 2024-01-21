(function () {
  'use strict';

  angular
    .module('app.main', [
      // 'multi-select-tree'
    ])
    .config(config);

  /** @ngInject */
  function config(msApiProvider) {
    //todo main method goes here

    /**
     * msApiProvider
     */

    //request
    msApiProvider.register('findRequests', ['CustomerRequest/findall']);
    msApiProvider.register('requestTypes', ['CustomerRequestType/findall']);
    msApiProvider.register('customRequestTypes', ['CustomerRequestType/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('injuryTypes', ['InjuryType/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('Departments', ['department/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('UserDepartments', ['department/findall']);
    msApiProvider.register('Regions', ['region/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('RequestStatusList', ['RequestStatus/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('addRequest', ['CustomerRequest/add']);
    msApiProvider.register('delRequest', ['CustomerRequest/remove']);
    msApiProvider.register('addRating', ['Rating/add']);
    msApiProvider.register('updateRequest', ['CustomerRequest/update']);
    msApiProvider.register('updateRating', ['Rating/update']);
    msApiProvider.register('updateRequestStatusHistory', ['RequestStatusHistory/add']);
    msApiProvider.register('addTask', ['MainTask/add']);
    msApiProvider.register('sendEmail', ['RequestEmail/add']);
    msApiProvider.register('updateTask', ['MainTask/update']);
    msApiProvider.register('getUser', ['User/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('sourceTypes', ['SourceType/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);

    msApiProvider.register('findSettings', ['UserSettings/findall']);
    msApiProvider.register('addSettings', ['UserSettings/add']);
    msApiProvider.register('delSettings', ['UserSettings/remove']);

    msApiProvider.register('getUserById', ['User/getById', null, { query: { method: 'get', isArray: false, cancellable: true } }]);


    //task

    msApiProvider.register('findTask', ['MainTask/findall']); //, null, { save: { method: 'post', isArray: true, cancellable: true } }
    msApiProvider.register('findComment', ['Comment/findall']);
    msApiProvider.register('findHistory', ['TaskHistory/findall']);
    msApiProvider.register('Projects', ['project/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('TaskStatusList', ['TaskStatus/getall', null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('updateTaskStatusHistory', ['TaskStatusHistory/add']);
    msApiProvider.register('addTaskComment', ['Comment/add']);
    msApiProvider.register('addAttachment', ['attachment/add']);
    msApiProvider.register('addProject', ['project/add']);
    msApiProvider.register('updateProject', ['project/update']);
    msApiProvider.register('removeProject', ['project/remove']);
    msApiProvider.register('delTask', ['MainTask/remove']);
    msApiProvider.register('addDepartment', ['department/add']);
    msApiProvider.register('updateDepartment', ['department/update']);
    msApiProvider.register('removeDepartment', ['department/remove']);
    msApiProvider.register('getTaskById', ['maintask/getbyid']);
    msApiProvider.register('findPhoneNumber', ['phonenumber/find']);
    // msApiProvider.register('updateTaskComment', ['Comment/update']);



    msApiProvider.register('findLawProcess', ['LawProcess/findall']);
    msApiProvider.register('addLawProcess', ['LawProcess/add']);
    msApiProvider.register('updateLawProcess', ['LawProcess/update']);
    msApiProvider.register('getCourt', ['LawProcess/getCourt',null, { query: { method: 'get', isArray: false, cancellable: true } }]);
    msApiProvider.register('getJudge', ['LawProcess/getJudge',null, { query: { method: 'get', isArray: false, cancellable: true } }]);
  }

})();
