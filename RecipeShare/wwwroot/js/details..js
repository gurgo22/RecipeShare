var chartLabels = @Html.Raw(Json.Serialize(proteinData.Select(i => i.Name)));
var chartData = @Html.Raw(Json.Serialize(proteinData.Select(i => i.Protein)));

document.getElementById('servings').addEventListener('input', function () {

    const servings = parseInt(this.value, 10);
    var proteinPerServing = @Model.TotalProtein(1) * servings;
    document.getElementById('total-protein').textContent = (proteinPerServing).toFixed(0);
});

document.getElementById('rating').addEventListener('input', function () {

    document.getElementById('ratingValue').textContent = this.value;
});