import Transaction from './Transaction.js';

let baseUrl = "http://localhost:5252/v1"

class PaginatedResponse{
  constructor(currentPage, totalPages, pageSize, totalCount,data) {
    this.currentPage = currentPage;
    this.totalPages = totalPages;
    this.pageSize = pageSize;
    this.totalCount = totalCount;
    this.data = data;
  }
}

async function getTransactionsByPeriod(startDate, endDate, pageNumber, pageSize){
  try {
    if(startDate == null) startDate = "2024-07-01"
    if(endDate == null) endDate = "2024-07-31"

    const response = await fetch(`http://localhost:5252/v1/transactions?pageNumber=${pageNumber}&pageSize=${pageSize}`,{
      method:'GET',
      credentials:'include'
    });
    
    debugger
    const data = await response.json();
    const allTransactions = data.data.map(transaction => new Transaction(transaction));
    return new PaginatedResponse(data.currentPage,data.totalPages,data.pageSize,data.totalCount,allTransactions);    
  } catch (error) {
    console.error('Error fetching transactions:', error);
    throw error;
  }
}

export default getTransactionsByPeriod

