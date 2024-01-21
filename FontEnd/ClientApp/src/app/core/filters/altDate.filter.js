(function () {
    'use strict';

    angular
        .module('app.core')
        .filter('toDate', function() {
            return function(input) {
                return new Date(input);
            }
        })
        .filter('altDate', altDate);

    /** @ngInject */

    function altDate() {
        return function (value) {
            return moment(value).format('D.MM.YY HH:mm');
        }

    }
    // function altDate()
    // {
    //     return function (value)
    //     {
    //         var diff = Date.now() - new Date(value);
    //         moment.locale('az', {
    //             months : 'yanvar_fevral_mart_aprel_may_iyun_iyul_avqust_sentyabr_oktyabr_noyabr_dekabr'.split('_'),
    //             monthsShort : 'yan_fev_mar_apr_may_iyn_iyl_avq_sen_okt_noy_dek'.split('_'),
    //             weekdays : 'Bazar_Bazar ertəsi_Çərşənbə axşamı_Çərşənbə_Cümə axşamı_Cümə_Şənbə'.split('_'),
    //             weekdaysShort : 'Baz_BzE_ÇAx_Çər_CAx_Cüm_Şən'.split('_'),
    //             weekdaysMin : 'Bz_BE_ÇA_Çə_CA_Cü_Şə'.split('_'),
    //             weekdaysParseExact : true,
    //             longDateFormat : {
    //                 LT : 'HH:mm',
    //                 LTS : 'HH:mm:ss',
    //                 L : 'DD.MM.YYYY',
    //                 LL : 'D MMMM YYYY',
    //                 LLL : 'D MMMM YYYY HH:mm',
    //                 LLLL : 'dddd, D MMMM YYYY HH:mm'
    //             },
    //             calendar : {
    //                 sameDay : '[bugün saat] LT',
    //                 nextDay : '[sabah saat] LT',
    //                 nextWeek : '[gələn həftə] dddd [saat] LT',
    //                 lastDay : '[dünən] LT',
    //                 lastWeek : '[keçən həftə] dddd [saat] LT',
    //                 sameElse : 'L'
    //             },
    //             relativeTime : {
    //                 future : '%s sonra',
    //                 past : '%s əvvəl',
    //                 s : 'az',
    //                 m : 'bir dəqiqə',
    //                 mm : '%d dəqiqə',
    //                 h : 'bir saat',
    //                 hh : '%d saat',
    //                 d : 'bir gün',
    //                 dd : '%d gün',
    //                 M : 'bir ay',
    //                 MM : '%d ay',
    //                 y : 'bir il',
    //                 yy : '%d il'
    //             },
    //             meridiemParse: /gecə|səhər|gündüz|axşam/,
    //             isPM : function (input) {
    //                 return /^(gündüz|axşam)$/.test(input);
    //             },
    //             meridiem : function (hour, minute, isLower) {
    //                 if (hour < 4) {
    //                     return 'gecə';
    //                 } else if (hour < 12) {
    //                     return 'səhər';
    //                 } else if (hour < 17) {
    //                     return 'gündüz';
    //                 } else {
    //                     return 'axşam';
    //                 }
    //             },
    //             ordinalParse: /\d{1,2}-(ıncı|inci|nci|üncü|ncı|uncu)/,
    //             ordinal : function (number) {
    //                 if (number === 0) {  // special case for zero
    //                     return number + '-ıncı';
    //                 }
    //                 var a = number % 10,
    //                     b = number % 100 - a,
    //                     c = number >= 100 ? 100 : null;
    //                 return number + (suffixes[a] || suffixes[b] || suffixes[c]);
    //             },
    //             week : {
    //                 dow : 1, // Monday is the first day of the week.
    //                 doy : 7  // The week that contains Jan 1st is the first week of the year.
    //             }
    //         });
    //        // console.log(moment.locale())
    //         /**
    //          * If in a hour
    //          * e.g. "2 minutes ago"
    //          */
    //         if ( diff < (60 * 60 * 1000) )
    //         {
    //             return moment(value).fromNow();
    //         }
    //         /*
    //          * If in the day
    //          * e.g. "11:23"
    //          */
    //         else if ( diff < (60 * 60 * 24 * 1000) )
    //         {
    //             return moment(value).format('HH:mm');
    //         }
    //         /*
    //          * If in week
    //          * e.g "Tuesday"
    //          */
    //         else if ( diff < (60 * 60 * 24 * 7 * 1000) )
    //         {
    //             return moment(value).format('dddd');
    //         }
    //         /*
    //          * If more than a week
    //          * e.g. 03/29/2016
    //          */
    //         else
    //         {
    //             return moment(value).calendar();
    //         }

    //     };
    // }

})();