//
//  ManoProcessor.h
//  ManoSDK
//
//  Created by Mhretab on 26/04/16.
//  Copyright © 2016 ManoMotion. All rights reserved.
//

#ifndef ManoProcessor_h
#define ManoProcessor_h

#define WEAK_IMPORT __attribute__((weak))
#define EXTERN __declspec(dllexport)
#define MAX_CONTOUR_POINTS 200
#define RGB_FORMAT 0
#define PORTRAIT_ORIENTATION 1
#define INV_PORTRAIT_ORIENTATION 3
#define LANDSCAPE_ORIENTATION 2
#define INV_LANDSCAPE_ORIENTATION 0
#include "opencv2/opencv.hpp"
#include "sdk/public_structs.hpp"
#include <string>
#include <stdio.h>
#include  <ios>
enum AddOn
{
    ADD_ON_DEFAULT = 0,
    ADD_ON_ARKIT = 1,
    ADD_ON_VUFORIA = 3
};


using namespace std;

/*
 * This dataset collects all the useful information to be provided to the developer,
 *  It contains not only the data retrived from the the database but also the dynamic gesture, roi size and position and binary
 */

#define ENTRY_POINT __attribute__ ((visibility ("default")))
#define PROTECTED __attribute__ ((visibility ("protected")))
#define INTERNAL __attribute__ ((visibility ("internal")))
#define HIDDEN __attribute__ ((visibility ("hidden")))

extern "C"  {
    struct BoundingBox{
        cv::Point3f top_left;
        float width;
        float height;
    };
    struct TrackingInfo{
        BoundingBox bounding_box;
        cv::Point3f palm_center;
        float rotation;
        float relative_depth;
        int amount_of_contour_points;
        int amount_of_inner_points;
        cv::Point3f finger_tips[5];
        cv::Point3f contour_points[200];
        cv::Point3f inner_points[200];
    };
    
    struct GestureInfo{
        int mano_class;
        int mano_gesture_continuous;
        int mano_gesture_trigger;
        int state;
        int hand_side;
    };
    struct HandInfo {
        TrackingInfo tracking_info;
        GestureInfo gesture_info;
        int warning;
        int hand;
        void* cut_hand_rgb;
    };
#ifdef _LOG_MEASUREMENT_ON_
    vector<int> pre_process_values;
    vector<int> return_info_values;
    vector<int> whole_process_frame_values;
#endif
    /*
     * This method must be always called before anything in order to initialize all the necessary vbles
     */
    
    ENTRY_POINT int init(char * serial_key);
    
    ENTRY_POINT void processFrame(HandInfo* hand_info0,HandInfo* hand_info1,Session * session_info);
    
    ENTRY_POINT void calibrate();
#ifdef _LOG_MEASUREMENT_ON_
    ENTRY_POINT void stop();
#endif
    
    ENTRY_POINT void  setFrameArray (void * data);
    
    ENTRY_POINT void  setMRFrameArray(int  * data0,int  * data1);
    
    ENTRY_POINT void  setResolution(int width, int height);
    
    ENTRY_POINT void  setImageBinary(int * binary);
    
    
}
#endif /* ManoProcessor_h */
