var ctx = document.getElementById('proteinChart').getContext('2d');
var proteinChart = new Chart(ctx, {
    type: 'pie',
    data: {
        labels: chartLabels,
        datasets: [{
            data: chartData,
            backgroundColor: [
                '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40'
            ],
            hoverBackgroundColor: [
                '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40'
            ]
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Protein Content per Ingredient'
            }
        }
    }
});
