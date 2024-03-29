(function () {
    'use strict';

    angular
        .module('app.core')
        .filter('filterByArray', filterByArray);

    /** @ngInject */
    function filterByArray() {

        return function (items, parameter, ids) {
           // console.log(items);
            // var parameter = criteria.parameter
            // var ids = criteria.parameter
           // console.log(parameter, ids)
            if (items.length === 0 || !ids || ids.length === 0) {
                return items;
            }

            var filtered = [];
            //debugger;
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                var match = false;

                // for (var j = 0; j < ids.length; j++) {
                //    var id = ids[j];
                if (ids.indexOf(item[parameter]) > -1) {
                    match = true;
                    //break;
                    //   }
                }

                if (match) {
                    filtered.push(item);
                }

            }

            return filtered;

        };
    }

})();