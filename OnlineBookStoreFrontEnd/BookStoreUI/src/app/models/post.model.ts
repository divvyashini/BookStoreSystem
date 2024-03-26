export class Post {
    id: number;
    author: string;
    authorId:number;
    likes:number
    popularity:number;
    reads:number;
    tags:string[];


    constructor(id:number,author:string,authorId:number,likes:number,popularity:number,reads:number,tags:string[]) {
        this.id = id,
        this.author = author,
        this.authorId= authorId,
        this.likes = likes,
        this.popularity = popularity,
        this.reads = reads,
        this.tags = tags
      }

}