(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('RequestLawProcessDialogController', RequestLawProcessDialogController);

    /** @ngInject */
    function RequestLawProcessDialogController($mdDialog,
        $filter,
        msApi,
        SourceList,
        RequestTypes,
        Requests,
        RequestStatusList,
        ThisRequest,
        NotificationService,
        authService,
        lf,
        // LoadMoreIsInProgress,
        event) {
        var vm = this;

        // Data 
        //vm.customerRequestFilter = { customerRequestTypeId: id }
        vm.title = 'Bu məhkəmə işi ilə bağlı ətraflı məlumat';
        vm.sourceList = SourceList.list;
        vm.requests = Requests;
        //console.log(Requests); 
        vm.requestTypesFilter = RequestTypes;
        vm.requestStatusList = RequestStatusList
        //vm.sourceType = SourceType;
        vm.fromType = SourceList.from;
        vm.lawProcess = [];
        vm.courtList = [];
        vm.judgeList = [];
        vm.courtSelected = courtSelected;
        vm.judgeSelected = judgeSelected;
        vm.requestForm = ThisRequest;
        vm.saveRequest = saveRequest;
        // Methods
        //vm.saveEmail = saveEmail;
        vm.loadDataIsProgress = false;
        vm.showOffer = false;

        vm.closeDialog = closeDialog;
        vm.saveLawProcess = saveLawProcess;
        vm.addLawProcess = addLawProcess;
        vm.requestFileSelected = requestFileSelected;
       
        vm.getJudge = getJudge;
        loadData({ customerRequestId: ThisRequest.id })
        getIdOfParent(vm.requestTypesFilter, vm.requestForm.customerRequestTypeId, vm.requestForm.customerParentRequestTypeId)
        // console.log(vm.departmentList)

          /**
         * Save task
         */
        function saveRequest(requestForm) {

            msApi.request('updateRequest@save', requestForm,
                // SUCCESS
                function (response) {
                    if (response.isSuccess) {
                        NotificationService.show('Müraciət dəyişdirildi', 'success')
                        if (response.output.requestStatus.hasOwnProperty("updatedDate"))
                            response.output.requestStatus.createdDate = response.output.requestStatus.updatedDate
                    } else {
                        response.errorList.forEach(function (element) {
                            NotificationService.show(element.text, 'error')
                        }, this);
                    }

                    vm.requestForm = response.output;
                    var _requestExist = $filter('filter')(vm.requests, { id: response.output.id });
                    console.log(_requestExist);
                    if (_requestExist.length > 0) {
                        vm.requests[vm.requests.indexOf(_requestExist[0])] = response.output;
                    }
                },
                // ERROR
                function (response) {
                    console.error(response.output)
                }
            );

            closeDialog();
        }

        /**
         * Save task
         */

        function getIdOfParent(array, id, toId) {
            angular.forEach(array, function (element) {
                angular.forEach(element.childRequestTypes,function (type) {
                    if (type.id == id) {
                        vm.requestForm.customerParentRequestTypeId = type.parentRequestTypeId
                        return vm.requestForm.customerParentRequestTypeId;
                    }
                });
            });
        }
        function loadData(data) {
            vm.loadDataIsProgress = true;
            msApi.request('findLawProcess@save', data,
                // SUCCESS
                function (response) {
                    vm.loadDataIsProgress = false;
                    vm.lawProcess = response.output;
                    console.log(vm.lawProcess);
                },
                // ERROR
                function (response) {
                    vm.loadDataIsProgress = false;
                    console.error(response.output)
                });
            getCourt();
        }

        function getCourt() {
            msApi.request('getCourt@query',
                // SUCCESS
                function (response) {
                    vm.courtList = response.output;
                    console.log(vm.lawProcess);
                },
                // ERROR
                function (response) {
                    console.error(response.output)
                });
        }


        function getJudge(id) {
            msApi.request('getJudge@query', { id: id },
                // SUCCESS
                function (response) {
                    vm.judgeList = response.output;
                    console.log(vm.lawProcess);
                },
                // ERROR
                function (response) {
                    console.error(response.output)
                });
        }

        function courtSelected(proc, court) {
            vm.showOffer = false;
            proc.court = court.name;
            getJudge(court.id)
        }

        function judgeSelected(proc, judge) {
            vm.showOfferJudge = false;
            proc.judge = judge.name;
        }

        function addLawProcess() {
            vm.lawProcess.push({})
            vm.selectedIndex = vm.lawProcess.length;
        }
        function saveLawProcess(data) {
            console.log(data);
            data.inputDateTime = moment(data.inputDateTime).add(0, 'hours').format();
            data.orderDateTime = moment(data.orderDateTime).add(0, 'hours').format();
            console.log(data);
            data.customerRequestId = ThisRequest.id;
            msApi.request(data.id ? 'updateLawProcess@save' : 'addLawProcess@save', data,
                // SUCCESS
                function (response) {
                    console.log(response)
                    if (response.isSuccess) {
                        NotificationService.show(response.output.id + " nömrəli instansiya " + data.id ? "yeniləndi" : "əlavə edildi", 'success')
                    } else {
                        response.errorList.forEach(function (element) {
                            //NotificationService.show(element.text, 'error')
                        }, this);
                    }
                    // try {
                    //     $rootScope.$evalAsync(function () {
                    //         response.output.customerRequestType = { name: requestForm.placeHolder.requestType }
                    //         response.output.department = { name: requestForm.placeHolder.department }
                    //         vm.requests.unshift(response.output);
                    //     })

                    //     if (requestForm.createTask) {
                    //         OpenTaskDialog(event, response.output);
                    //     }
                    // }
                    // catch (err) {
                    //     console.log(err);
                    // }
                    //requestForm.createdDate = new Date();
                    // vm.requests.unshift(response);
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );

        }


        /**
         * Close dialog
         */
        function closeDialog() {
            $mdDialog.hide();
        }
        
        function requestFileSelected(e, reader, file, fileList, fileOjects, fileObj) {
            vm.requestForm.isProgress = true;
            //vm.commentFileName = file.filename;
            //console.log(file)
            var newFile = {
              // relationalId: vm.selectedTask.id,
              fileType: 2,
              mediaType: fileObj.filetype,
              container: fileObj.base64,
              description: fileObj.filename,
              extension: fileObj.filename.split('.').pop()
            }
            msApi.request('addAttachment@save', newFile,
              // SUCCESS
              function (response) {
                // requestForm.createdDate = new Date();
                // task.taskStatusId = statusId;
                // console.log(response);
                // vm.filesForUpload.push(response);
                vm.requestForm.attachment.push(response.output)
                // console.log(vm.requestForm);
                vm.requestForm.isProgress = false
              },
              // ERROR
              function (response) {
                //console.error(response.data)
                vm.requestForm.isProgress = false
              }
            );
            // console.log(newFile);
          }
  
    }
})();