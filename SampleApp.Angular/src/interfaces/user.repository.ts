import User from "../models/user.entity";
import { BaseRepository } from "./base.repository";

export interface UserRepository extends BaseRepository<User>{

}