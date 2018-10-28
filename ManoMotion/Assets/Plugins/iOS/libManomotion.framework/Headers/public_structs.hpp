//
//  public_structs.hpp
//  ManoMotion-iOS
//
//  Created by Julio chaná on 24/08/2018.
//  Copyright © 2018 ManoMotion. All rights reserved.
//

#ifndef public_structs_h
#define public_structs_h


struct Session{
    int flag;
    int current_plataform;
    int background_mode; // calibration info
    int image_format;
    int orientation;
    int calibration_value; // lower boundary
    int database_set_selection;
    int add_on;
    int version;
    int is_two_hands_enabled_by_developer;
    int two_hands_supported; // determined by the license
};


#endif /* public_structs_h */
