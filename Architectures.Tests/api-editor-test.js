import http from 'k6/http';
import { check } from 'k6';
import { sleep } from 'k6';

export default function () {
    const url = 'http://localhost:5000/api/v1/novo-projeto/atualizar-contadores?idVeiculo=EOMna&pagina=1&top=10&projetoStatus=[object Object]&projetoStatus=[object Object]&projetoStatus=[object Object]&projetoStatus=[object Object]&projetoStatus=[object Object]&projetoStatus=[object Object]&projetoStatus=[object Object]&status=2';
    const params = {
        headers: {
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InB1Ymxpc2hlcm54QG56bi5pbyIsImp0aSI6IjE1OTMiLCJzdWIiOiJQdWJsaXNoZXIgTlgiLCJuYmYiOjE2ODg3NjM3NTAsImV4cCI6MTY4ODkzNjU1MCwiaWF0IjoxNjg4NzYzNzUwLCJpc3MiOiJOWk4iLCJhdWQiOiJuZXhwZXJ0cy1hcHAifQ.GKNAvBrqCBuDRCgB7LhKXlDeoCzCtsMWCayxGS-MMkY'
        },
    };

    const res = http.get(url, params);

    sleep(1);

    const checkRes = check(res, {
        'status is 200': (r) => r.status === 200
    });
}