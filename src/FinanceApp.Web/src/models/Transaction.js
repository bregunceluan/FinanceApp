import Category from "./Category";

class Transaction{
    constructor({ id, title, createdAt, paidOrReceivedAt, type, amount, categoryId, category, userId }) {
        this.id = id;
        this.title = title;
        this.createdAt = new Date(createdAt);
        this.paidOrReceivedAt = new Date(paidOrReceivedAt);
        this.type = type;
        this.amount = amount;
        this.categoryId = categoryId;
        this.category = category ? new Category(category) : null;
        this.userId = userId;
    }}

export default Transaction;
