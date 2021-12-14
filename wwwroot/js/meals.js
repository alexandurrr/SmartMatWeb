let category = document.getElementById("category");
let vegetarian = document.getElementById("vegetarian");
let glutenfree = document.getElementById("glutenfree");

async function updateCheckboxes(){
    let result = await fetch("/Meals/GetFilteredRecipes?category="+category.innerText+"&vegetarian="+vegetarian.checked+"&glutenfree="+glutenfree.checked);
    let htmlResult = await result.text();
    $('#resultDiv').html(htmlResult);
    if (!htmlResult.includes("resultRow")){
        $("#resultDiv").html("<p>Finner ingen oppskrifter med denne filtreringen.</p>");
    }
    
}

$('#vegetarian').change(updateCheckboxes);
$('#glutenfree').change(updateCheckboxes);
updateCheckboxes();