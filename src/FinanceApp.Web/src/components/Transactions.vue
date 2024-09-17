<template>
    <v-container class="bg-red" style="height: 100%;" fluid>
        
        <v-row>          
            <v-col style="height: 46em;">
                <h1>Transactions</h1>
                <v-card style="height: 90%;">
                    <v-data-table :headers="headers" :items="transactions">
                    </v-data-table>
                </v-card>
            </v-col>
        </v-row>
        
        <v-row>
            <v-col xs="1"></v-col>
            <v-col xs="2">
                <v-pagination v-model="currentPage" :total-visible="totalVisiblePagination" :length="pages"></v-pagination>
            </v-col>
            <v-col xs="1">{{ page }}</v-col>    
        </v-row>
        
    </v-container>
</template>

<script setup>
import { onMounted } from 'vue';
import getTransactionsByPeriod from '@/models/FinanceApi'
import login from '@/models/authFunctions'


const headers = [
{title:"Category", key:"category"},
{title:"Title", key:"title"},   
{title:"CreatedAt", key:"createdAt"},
{title:"Paid/ReceivedAt", key:"paidReceivedAt"},
{title:"Amount", key:"amount"},   
]

onMounted(async () =>{
    debugger

    await login()
    let transactions = await getTransactionsByPeriod("2024-07-01","2024-07-31",1,3)
})


const currentPage = ref(1);
const pages = 5
let totalVisiblePagination = 5

const transactions = [
    { category: "1", title: "Passagem GRU - CNF", createdAt: "2023-07-01", paidReceivedAt: "2023-07-01", amount: "235.15" },
    { category: "2", title: "Almoço Restaurante", createdAt: "2023-07-02", paidReceivedAt: "2023-07-02", amount: "45.90" },
    { category: "3", title: "Mercado", createdAt: "2023-07-03", paidReceivedAt: "2023-07-03", amount: "150.75" },
    { category: "4", title: "Assinatura Netflix", createdAt: "2023-07-04", paidReceivedAt: "2023-07-04", amount: "29.90" },
    { category: "1", title: "Passagem GRU - BSB", createdAt: "2023-07-05", paidReceivedAt: "2023-07-05", amount: "320.00" },
    { category: "2", title: "Jantar Restaurante", createdAt: "2023-07-06", paidReceivedAt: "2023-07-06", amount: "80.50" },
    { category: "3", title: "Farmácia", createdAt: "2023-07-07", paidReceivedAt: "2023-07-07", amount: "70.30" },
    { category: "4", title: "Spotify Premium", createdAt: "2023-07-08", paidReceivedAt: "2023-07-08", amount: "16.90" },
    { category: "1", title: "Passagem GRU - POA", createdAt: "2023-07-09", paidReceivedAt: "2023-07-09", amount: "275.00" },
    { category: "2", title: "Lanche Rápido", createdAt: "2023-07-10", paidReceivedAt: "2023-07-10", amount: "20.00" },
    { category: "3", title: "Supermercado", createdAt: "2023-07-11", paidReceivedAt: "2023-07-11", amount: "200.00" },
    { category: "4", title: "Assinatura Amazon Prime", createdAt: "2023-07-12", paidReceivedAt: "2023-07-12", amount: "9.90" },
    { category: "1", title: "Passagem GRU - RIO", createdAt: "2023-07-13", paidReceivedAt: "2023-07-13", amount: "180.00" },
    { category: "2", title: "Café", createdAt: "2023-07-14", paidReceivedAt: "2023-07-14", amount: "12.00" },
    { category: "3", title: "Padaria", createdAt: "2023-07-15", paidReceivedAt: "2023-07-15", amount: "35.50" },
    { category: "4", title: "Apple Music", createdAt: "2023-07-16", paidReceivedAt: "2023-07-16", amount: "21.90" },
    { category: "1", title: "Passagem GRU - REC", createdAt: "2023-07-17", paidReceivedAt: "2023-07-17", amount: "400.00" },
    { category: "2", title: "Jantar com amigos", createdAt: "2023-07-18", paidReceivedAt: "2023-07-18", amount: "95.00" },
    { category: "3", title: "Material de Escritório", createdAt: "2023-07-19", paidReceivedAt: "2023-07-19", amount: "45.60" },
    { category: "4", title: "Disney+", createdAt: "2023-07-20", paidReceivedAt: "2023-07-20", amount: "27.90" },
    { category: "1", title: "Passagem GRU - SSA", createdAt: "2023-07-21", paidReceivedAt: "2023-07-21", amount: "210.00" },
    { category: "2", title: "Almoço Executivo", createdAt: "2023-07-22", paidReceivedAt: "2023-07-22", amount: "60.00" },
    { category: "3", title: "Livraria", createdAt: "2023-07-23", paidReceivedAt: "2023-07-23", amount: "120.90" },
    { category: "4", title: "HBO Max", createdAt: "2023-07-24", paidReceivedAt: "2023-07-24", amount: "34.90" },
    { category: "1", title: "Passagem GRU - CWB", createdAt: "2023-07-25", paidReceivedAt: "2023-07-25", amount: "190.00" },
    { category: "2", title: "Happy Hour", createdAt: "2023-07-26", paidReceivedAt: "2023-07-26", amount: "50.00" },
    { category: "3", title: "Supermercado", createdAt: "2023-07-27", paidReceivedAt: "2023-07-27", amount: "250.00" },
    { category: "4", title: "Paramount+", createdAt: "2023-07-28", paidReceivedAt: "2023-07-28", amount: "19.90" },
    { category: "1", title: "Passagem GRU - BEL", createdAt: "2023-07-29", paidReceivedAt: "2023-07-29", amount: "320.00" },
    { category: "2", title: "Lanche", createdAt: "2023-07-30", paidReceivedAt: "2023-07-30", amount: "15.00" },
    { category: "3", title: "Açougue", createdAt: "2023-07-31", paidReceivedAt: "2023-07-31", amount: "90.00" },
    { category: "4", title: "Google Drive", createdAt: "2023-08-01", paidReceivedAt: "2023-08-01", amount: "12.00" },
    { category: "1", title: "Passagem GRU - MAO", createdAt: "2023-08-02", paidReceivedAt: "2023-08-02", amount: "450.00" },
    { category: "2", title: "Café da Manhã", createdAt: "2023-08-03", paidReceivedAt: "2023-08-03", amount: "20.00" },
    { category: "3", title: "Farmácia", createdAt: "2023-08-04", paidReceivedAt: "2023-08-04", amount: "60.00" },
    { category: "4", title: "YouTube Premium", createdAt: "2023-08-05", paidReceivedAt: "2023-08-05", amount: "24.90" },
    { category: "1", title: "Passagem GRU - NAT", createdAt: "2023-08-06", paidReceivedAt: "2023-08-06", amount: "310.00" },
    { category: "2", title: "Jantar", createdAt: "2023-08-07", paidReceivedAt: "2023-08-07", amount: "75.00" },
    { category: "3", title: "Supermercado", createdAt: "2023-08-08", paidReceivedAt: "2023-08-08", amount: "180.00" },
    { category: "4", title: "Amazon Prime Video", createdAt: "2023-08-09", paidReceivedAt: "2023-08-09", amount: "14.90" },
    { category: "1", title: "Passagem GRU - BSB", createdAt: "2023-08-10", paidReceivedAt: "2023-08-10", amount: "270.00" },
    { category: "2", title: "Almoço", createdAt: "2023-08-11", paidReceivedAt: "2023-08-11", amount: "40.00" },
    { category: "3", title: "Loja de Roupas", createdAt: "2023-08-12", paidReceivedAt: "2023-08-12", amount: "300.00" },
    { category: "4", title: "Twitch", createdAt: "2023-08-13", paidReceivedAt: "2023-08-13", amount: "19.90" },
    { category: "1", title: "Passagem GRU - FOR", createdAt: "2023-08-14", paidReceivedAt: "2023-08-14", amount: "260.00" },
    { category: "2", title: "Jantar", createdAt: "2023-08-15", paidReceivedAt: "2023-08-15", amount: "90.00" },
    { category: "3", title: "Supermercado", createdAt: "2023-08-16", paidReceivedAt: "2023-08-16", amount: "220.00" },
    { category: "4", title: "Vimeo", createdAt: "2023-08-17", paidReceivedAt: "2023-08-17", amount: "25.00" },
    { category: "1", title: "Passagem GRU - CGB", createdAt: "2023-08-18", paidReceivedAt: "2023-08-18", amount: "290.00" },
    { category: "2", title: "Café da Tarde", createdAt: "2023-08-19", paidReceivedAt: "2023-08-19", amount: "10.00" },
    { category: "3", title: "Padaria", createdAt: "2023-08-20", paidReceivedAt: "2023-08-20", amount: "35.00" },
    { category: "4", title: "Dropbox", createdAt: "2023-08-21", paidReceivedAt: "2023-08-21", amount: "11.90" }
];


let onScreenExpanses = []

</script>