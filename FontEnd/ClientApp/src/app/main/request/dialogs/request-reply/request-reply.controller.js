(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('RequestReplyDialogController', RequestReplyDialogController);

    /** @ngInject */
    function RequestReplyDialogController($mdDialog,
        $filter,
        msApi,
        ThisRequest,
        NotificationService,
        authService,
        // LoadMoreIsInProgress,
        event) {
        var vm = this;

        // Data 
        //vm.customerRequestFilter = { customerRequestTypeId: id }
        vm.title = 'Bu müraciətlə bağlı göndərilmiş emaillər';

        vm.requestForm = ThisRequest;
            console.log(vm.requestForm);
        vm.emailForm = {
            subject: "Re: " + (ThisRequest.department ? ThisRequest.department.name : "") + "| " + ThisRequest.customerRequestType.name,
            email: ThisRequest.email || "software.support@aztelekom.az",
            customerRequestId: ThisRequest.id,
            // message:"<b>test</b>"
        }
        // Methods
        vm.saveEmail = saveEmail;

        vm.closeDialog = closeDialog;
        // console.log(vm.departmentList)

        /**
         * Save task
         */

        function saveEmail(emailForm) {
            return msApi.request('getUserById@query', { id: authService.authentication.userId },
                // SUCCESS
                function (responseUser) {
                    if (responseUser.isSuccess) {
                        var userData = responseUser.output
                        emailForm.message += '<br/><div>'
                        +'<div style="margin:0;"><font face="Times New Roman,serif" size="3"><span style="font-size:12pt;"><font face="Arial,sans-serif" color="#1F4E79"><span lang="en-US">'
                        + userData.firstName + " " + userData.lastName
                        +'</span></font></span></font></div>'
                        +'<div style="margin:0;"><font face="Times New Roman,serif" size="3"><span style="font-size:12pt;"><font face="Arial,sans-serif" color="#1F4E79"><span lang="en-US">&nbsp;</span></font></span></font></div>'
                        +'</div>'
                        msApi.request('sendEmail@save', emailForm,
                            // SUCCESS
                            function (response) {
                                if (response.isSuccess) {
                                    NotificationService.show('Email göndərildi', 'success')
                                } else {
                                    response.errorList.forEach(function (element) {
                                        NotificationService.show(element.text, 'error')
                                    }, this);
                                }
                            },
                            // ERROR
                            function (response) {
                                console.error(response.data)
                            }
                        );

                    } else {
                        closeDialog();
                    }
                    closeDialog();
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
    }
})();