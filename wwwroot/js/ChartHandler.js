function DesenharChart(ctx, cor, labels, values) {

    new Chart(ctx, {
        type: 'pie',
        data: {

            labels: labels,
            datasets: [{
                data: values,
                backgroundColor: GetCores(cor,1),
                borderColor: GetCores(cor, 2),
                borderWidth: 1
            }]
        },
        options: {
            plugins: {
                labels: {
                    render: 'value',
                    fontSize: 18,
                    fontStyle: 'bold',
                    fontColor: '#000'
                }
            }
        }
    });
}



function GetCores(numero, tipo) {

    var cores = [];

    switch (numero) {
        case 1:
            if (tipo === 1) {
                cores = [
                    'rgba(34, 191, 13, 0.5)',
                    'rgba(191, 13, 13, 0.5)',
                ];
            } else {
                cores = [
                    'rgba(34, 191, 13, 1)',
                    'rgba(191, 13, 13, 1)',
                ];
            }
            break;

        case 2:
            if (tipo === 1) {
                cores = [
                    'rgba(13, 52, 191, 0.5)',
                    'rgba(191, 13, 191, 0.5)',
                ];
            } else {
                cores = [
                    'rgba(13, 52, 191, 1)',
                    'rgba(191, 13, 191, 1)',
                ];
            }
            break;

        case 3:
            if (tipo === 1) {
                cores = [
                    'rgba(13, 191, 129, 0.5)',
                    'rgba(191, 185, 13, 0.5)',
                    'rgba(191, 96, 13, 0.5)',
                    'rgba(191, 49, 13, 0.5)',
                ];
            } else {
                cores = [
                    'rgba(13, 191, 129, 1)',
                    'rgba(191, 185, 13, 1)',
                    'rgba(191, 96, 13, 1)',
                    'rgba(191, 49, 13, 1)',
                ];
            }
            break;

        case 4:
            if (tipo === 1) {
                cores = [
                    'rgba(255, 200, 10, 0.5)',
                    'rgba(179, 30, 75, 0.5)',
                    'rgba(31, 191, 13, 0.5)',
                    'rgba(191, 129, 13, 0.5)',
                    'rgba(143, 13, 191, 0.5)',
                    'rgba(191, 13, 13, 0.5)',
                    'rgba(143, 13, 191, 0.5)',
                    'rgba(13, 52, 191, 0.5)',
                    'rgba(191, 13, 191, 0.5)',
                    'rgba(31, 191, 13, 0.5)',
                    'rgba(191, 129, 13, 0.5)',
                    'rgba(191, 13, 13, 0.5)',
                ];
            } else {
                cores = [
                    'rgba(255, 200, 10, 1)',
                    'rgba(179, 30, 75, 1)',
                    'rgba(31, 191, 13, 1)',
                    'rgba(191, 129, 13, 1)',
                    'rgba(143, 13, 191, 1)',
                    'rgba(191, 13, 13, 1)',
                    'rgba(143, 13, 191, 1)',
                    'rgba(13, 52, 191, 1)',
                    'rgba(191, 13, 191, 1)',
                    'rgba(31, 191, 13, 1)',
                    'rgba(191, 129, 13, 1)',
                    'rgba(191, 13, 13, 1)',
                ];
            }
            break;
    }

    console.log(cores);

    return cores;
}