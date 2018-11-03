(function (app) {
    'use strict';

    // ajax helper
    app.ajax = (function () {

        // creates an ajax get
        function ajaxGet(url, callback) {
            var request = buildXMLHttpResquest(callback);
            request.open('GET', url, true);
            request.setRequestHeader('Cache-Control', 'no-cache, no-store, must-revalidate');
            request.setRequestHeader('Pragma', 'no-cache');
            request.setRequestHeader('Expires', '-1');
            request.send();
        }

        // creates an ajax post
        function ajaxPost(url, data, callback) {
            var request = buildXMLHttpResquest(callback);
            request.open('POST', url, true);
            request.setRequestHeader('Content-type', 'application/json');
            request.send(data);
        }

        // handles the service response
        function success(callback) {
            if (this.readyState === 4 && this.status === 200) {
                var data = '';

                data = JSON.parse(this.responseText);

                callback(data, false);
            } else {
                callback(this.responseText, true);
            }
        }

        function failure(e) {
            console.log('** An error occurred during the transaction');
        }

        // creates an XMLHttpRequest
        function buildXMLHttpResquest(callback) {
            var request = new XMLHttpRequest();
            request.onload = success.bind(request, callback);
            request.onerror = failure;
            return request;
        }

        return {
            get: ajaxGet,
            post: ajaxPost
        };
    })();
}(convertApp));