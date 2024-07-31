import Transaction from '../models/Transaction.js';

class FinanceApiAPP {
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
  }

  async fetchTransactions(startDate,endDate,pageNumber,pageSize=30) {
    try {
      const response = await fetch(`${this.baseUrl}/transactions?pageNumber=${pageNumber}&pageSize=${pageSize}`);
      const data = await response.json();
      return data.data.map(transaction => new Transaction(transaction));
    } catch (error) {
      console.error('Error fetching transactions:', error);
      throw error;
    }
  }
}

export default ApiService;