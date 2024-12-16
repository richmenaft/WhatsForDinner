import { TagType } from '@/TypeScriptFiles/TagTypes'
import { TypeImage } from '@/TypeScriptFiles/Types'

export interface ITag {
  title: string
  type: TagType
  state?: number
}

export interface IDish {
  dishId: string
  title: string
  tags: ITag[]
  description: string
  img: TypeImage
}

export interface IRandomDish {
  title: string
  tags: ITag[]
  description: string
  img: TypeImage
}
