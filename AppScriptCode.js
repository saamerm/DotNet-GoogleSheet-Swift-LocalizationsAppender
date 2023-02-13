function doPost(request){ // DoPost in order to get postData
  var resultObject = JSON.parse(request.postData.contents);
  var result = processResult(resultObject);
  return ContentService
    .createTextOutput(JSON.stringify(result))
    .setMimeType(ContentService.MimeType.JSON);
}

function processResult(resultObject)
{
  // Open Google Sheet using ID
  // https://developers.google.com/sheets/api/guides/concepts Spreadsheet -> Sheet -> Cell
  var spreadSheet = SpreadsheetApp.openById("YOUR ID");
  var sheet = spreadSheet.getSheetByName("CodeConverter-DontEdit")
  var result = {"LanguageFile": "FAILED", "Line": "Unknown Failure"};
  try{
    // Get a cell value from a sheet
    var rowData = sheet.getRange(resultObject.Line,resultObject.Language).getValue() 
    var fileName = sheet.getRange(2,resultObject.Language).getValue() 
    result = {"LanguageFile": fileName, "Line": rowData};
  }catch(exc){
    // If error occurs, throw exception
    result = {"LanguageFile": "FAILED", "Line": exc};
  }
  // Return result
  return result;
}

function test(){
  var Line = 25
  var Language = 10
  var myJSObject='{"Line": "' + Line + 
                 '", "Language": "' + Language + 
                 '"}';
                 // Line doesnt need "" since it's an integer
  var result = processResult(JSON.parse(myJSObject))
  console.log(result)
}

// https://script.google.com/macros/s/YourUrl/exec