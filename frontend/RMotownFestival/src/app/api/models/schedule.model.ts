import { Artist } from './artist.model';
import { Stage } from './stage.model';

export interface Schedule {
  items: ScheduleItem[];
}

export interface ScheduleItem {
  id: number;
  artist: Artist;
  stage: Stage;
  time: string;
  isFavorite: boolean;
}
