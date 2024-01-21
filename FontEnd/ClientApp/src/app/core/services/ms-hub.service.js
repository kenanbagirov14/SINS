(function () {
    'use strict';

    angular
        .module('app.core')
        .factory('msHub', msHub);

    /** @ngInject */
    function msHub(Hub,  __env, authService) {
        // Private variables   
        // var authData = localStorageService.get('authorizationData');
        var authData = authService.authentication;
        //console.log(authData)
        var userId = 0;
        var token = '';
        if (authData) {
            userId = authData.userId;
            token = authData.token;
        }
        var service = {
            hub: getHub
        };
        var hub = (new Hub()).signalR
        // var connection = new hub.HubConnectionBuilder().withUrl("/chatHub").build();
        var connection = new hub.HubConnectionBuilder()
            .withUrl(__env.signalRUrl + "/?access_token=" + token)
            .configureLogging(hub.LogLevel.Information)
            .build();
        connection.start().catch(function (err) { console.error(err.toString()) });
        return service;


        // console.log(hub);
        function getHub() {

            //console.log(connection);
            // var connection = hub.HubConnectionBuilder()
            //     .withUrl("/chatHub")
            //     .configureLogging(hub.signalR.LogLevel.Information)
            //     .build();
            // connection.start()
            // // {

            //     //client side methods
            //     listeners: {
            //         'keepalive': function (response) {
            //             console.log(response)
            //         }
            //     },

            //     // server side methods
            //     // methods: ['received'],

            //     //query params sent on initial connection
            //     queryParams: {
            //         'userid': userId,
            //         'token': token
            //     },



            //     // //handle connection error
            //     errorHandler: function (error) {
            //         console.log(error);
            //     },
            //     //8,10,12,14,15
            //     //specify a non default root
            //     rootPath: __env.signalRUrl,
            //     //rootPath: 'http://192.168.77.84:45455/signalr/hubs',

            //     stateChanged: function (state) {
            //         switch (state.newState) {
            //             case $.signalR.connectionState.connecting:
            //                 console.log('connecting')
            //                 break;
            //             case $.signalR.connectionState.connected:
            //                 console.log('connected')
            //                 break;
            //             case $.signalR.connectionState.reconnecting:
            //                 console.log('reconnecting')
            //                 break;
            //             case $.signalR.connectionState.disconnected:
            //                 console.log('disconnected')
            //                 break;
            //         }
            //     }
            // });
            return connection;
        }

    }
}());