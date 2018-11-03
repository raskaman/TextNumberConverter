(function (app) {
    'use strict';

    app.indexPage = (function () {
        
        // page elements
        var elements = {
            textInput: document.getElementById('textInput'),
            submitInput: document.getElementById('submitInput'),
            resultLabel: document.getElementById('resultLabel')
        };

        // set event handlers
        function attachEvents() {
            elements.submitInput.addEventListener('click', function (e) {
                convertText();
            }, false);
        }

        // validates the text
        // call ajax service to convert the text
        function convertText() {
            var data = elements.textInput.value;

            if (/[^A-Za-z\d\s\.\"\”\“]/.test(data))
            {
                elements.resultLabel.innerHTML = "Invalid input, please try again!";
            }
            else {
                app.ajax.post('/api/TextConvert', JSON.stringify(data), convertCallback);
            }
        }

        // displays the result of the converted text
        function convertCallback(data, error) {
            if (error) {
                elements.textInput.innerHTML = "An unexpected error occured, please try again!";
            }
            else {
                elements.resultLabel.innerHTML = data;
            }
        }
      
        function init() {
            attachEvents();
        }

        return {
            init: init
        };
    })();

}(convertApp));