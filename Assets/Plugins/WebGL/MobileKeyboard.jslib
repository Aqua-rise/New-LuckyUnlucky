mergeInto(LibraryManager.library, {
  showMobileKeyboard: function (unityObjectPtr, callbackMethodPtr, fieldIdPtr) {
    var unityObject = UTF8ToString(unityObjectPtr);
    var callbackMethod = UTF8ToString(callbackMethodPtr);
    var fieldId = UTF8ToString(fieldIdPtr);

    var input = document.getElementById("mobileInput");
    if (!input) {
      input = document.createElement("input");
      input.id = "mobileInput";
      input.type = "text";
      input.style.position = "absolute";
      input.style.zIndex = 1000;
      input.style.top = "10px";
      input.style.left = "10px";
      input.style.fontSize = "16px";
      input.style.display = "none";
      document.body.appendChild(input);
    }

    input.value = "";
    input.style.display = "block";
    input.focus();

    input.oninput = function () {
      var message = fieldId + "|" + input.value;
      SendMessage(unityObject, callbackMethod, message);
    };

    input.onblur = function () {
      input.style.display = "none";
    };
  }
});
