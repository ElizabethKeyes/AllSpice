import { Profile } from "./Account.js"

export class Comment {
  constructor(data) {
    this.id = data.id
    this.body = data.body
    this.accountId = data.accountId
    this.recipeId = data.recipeId
    this.creator = new Profile(data.creator)
  }
}