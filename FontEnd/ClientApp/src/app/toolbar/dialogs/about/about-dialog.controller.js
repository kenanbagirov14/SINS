(function () {
    'use strict';

    angular
        .module('app.toolbar')
        .controller('AboutController', AboutController);

    /** @ngInject */
    function AboutController($mdDialog,
        $filter,
        msApi,  
        NotificationService,
        __env,
       // LoadMoreIsInProgress,
        event) {
        var vm = this;
        vm.env = __env;
        // Data 
          //vm.customerRequestFilter = { customerRequestTypeId: id }
        vm.title = vm.env.appName + " ver:"+vm.env.version;   
       
        vm.closeDialog = closeDialog;
              // console.log(vm.departmentList)
       
        /**
         * Save task
         */
        // function saveEmail(emailForm) {
        //     msApi.request('sendEmail@save', emailForm,
        //         // SUCCESS
        //         function (response) {
        //             if (response.isSuccess) {
        //                 NotificationService.show('Email göndərildi', 'success')
        //               } else {
        //                 response.errorList.forEach(function (element) {
        //                   NotificationService.show(element.text, 'error')
        //                 }, this);
        //               }
        //         },
        //         // ERROR
        //         function (response) {
        //             console.error(response.data)
        //         }
        //     );
        //     closeDialog();
        // }

        /**
         * Close dialog
         */
        function closeDialog() {
            $mdDialog.hide();
        }
    }
})();