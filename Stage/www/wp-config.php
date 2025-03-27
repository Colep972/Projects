<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the
 * installation. You don't have to use the web site, you can
 * copy this file to "wp-config.php" and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * MySQL settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://codex.wordpress.org/Editing_wp-config.php
 *
 * @package WordPress
 */

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define('DB_NAME', 'couleursmz492');

/** MySQL database username */
define('DB_USER', 'couleursmz492');

/** MySQL database password */
define('DB_PASSWORD', 'JvMyCESczZYb');

/** MySQL hostname */
define('DB_HOST', 'couleursmz492.mysql.db:3306');

/** Database Charset to use in creating database tables. */
define('DB_CHARSET', 'utf8');

/** The Database Collate type. Don't change this if in doubt. */
define('DB_COLLATE', '');

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define('AUTH_KEY',         'FmbqFB2TqLsquluR3HazWuO5nCljXNgoAFFYAfalS8vmFL3jK3vwBbV1lR8B');
define('SECURE_AUTH_KEY',  'b+UhoIlLO+Z1eoiFA+MGli0TnHMTxqqriLJH1pUaWoIJLjA0ncc3O8xFIe+g');
define('LOGGED_IN_KEY',    '12wjymbVAElcfYt4B61Mo5RbkDNg9jQpjHPb1rZ9X3hS4Zx2RlfmSzJ0wqge');
define('NONCE_KEY',        'CzXm2OEfFEcMHEQu2oFKq1uKzPuJqB0KbR2Iy8dt13s5ViTQMTD3i9q4xEJ1');
define('AUTH_SALT',        'YtKPv2KLKEGsDWTgq/C9vA/exErqzl6HCsh0aMJolI3S75nt4XwpP8Bb5omf');
define('SECURE_AUTH_SALT', 'QJ5hbj56EIFFtzoeWElhLERx+PZaVmGROKqLF3lfTyqV8D/ZgIUi62CwOe4q');
define('LOGGED_IN_SALT',   'OQGPGiuvRiGu7dE5R0NuUmo0Y2J6PF/px1wRWe+0h5PjSJTApBtQV/ZLIBFJ');
define('NONCE_SALT',       'vePf9x+r+9Oc0SfxDIHIKLBzvFF58hlRvcubmGYZqGAlFXiIxW3VVDjuXHht');

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 */
$table_prefix  = 'mod848_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the Codex.
 *
 * @link https://codex.wordpress.org/Debugging_in_WordPress
 */
define('WP_DEBUG', false);

/* That's all, stop editing! Happy blogging. */

/** Absolute path to the WordPress directory. */
if ( !defined('ABSPATH') )
	define('ABSPATH', dirname(__FILE__) . '/');

/* Fixes "Add media button not working", see http://www.carnfieldwebdesign.co.uk/blog/wordpress-fix-add-media-button-not-working/ */
define('CONCATENATE_SCRIPTS', false );

/** Sets up WordPress vars and included files. */
require_once(ABSPATH . 'wp-settings.php');

